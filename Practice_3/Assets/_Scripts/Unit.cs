using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public abstract class Unit : MonoBehaviour
{
    Vector3 targetPosition;    
    bool isActivated = false;
    int maxHp;
    protected int curHp;
    int ATKDamage;
    float moveSpeed; 
    float attackDelay = 0.5f;
    BoxCollider2D box;
    Rigidbody2D body;
    public Action<Unit> Destroyed;
    bool isInitialized = false;

    public void Initialize(int maxHp, int ATKDamage, float Speed = 0.5f)
    {
        if(isInitialized)
            return;
        this.maxHp = maxHp;
        this.ATKDamage = ATKDamage;
        this.moveSpeed = Speed;
        curHp = maxHp;

        isInitialized = true;
    }
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        box.isTrigger = true; 
    }
    public void Activate(Vector3 startPosition, Vector3 targetPosition)
    {
        transform.position = startPosition;
        this.targetPosition = targetPosition;
        if(!isActivated)
            transform.rotation = Quaternion.Euler(0,0,180);
        Vector3 dir = (targetPosition - startPosition).normalized;
        transform.rotation = Quaternion.LookRotation(transform.forward, dir);
        isActivated = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }
    public void DestroySelf()
    {
        isActivated = false;
        Destroyed?.Invoke(this);
        curHp = maxHp;
    }
    protected IEnumerator KnockBack()
    {   
        float elapsedTime=0f;
        while(elapsedTime < 0.1f)
        {
            this.transform.Translate(this.transform.forward * 5 * Time.deltaTime);
            elapsedTime+=Time.deltaTime;
            yield return null;
        }
    }
    
    public abstract void Attacked(int damage);
}