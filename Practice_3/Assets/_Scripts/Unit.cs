using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public abstract class Unit : MonoBehaviour
{
    protected bool canAttack = true;
    protected float elapsedTime = 0.5f;
    protected bool isClosed = false;
    Animator animator;
    protected bool isActivated = false;
    int maxHp;
    protected int curHp;
    protected int ATKDamage;
    protected float attackDelay = 0.5f;
    BoxCollider2D box;
    Rigidbody2D body;
    public Action<Unit> Destroyed;
    bool isInitialized = false;

    public bool GetActivate => isActivated;

    public void Initialize(int maxHp, int ATKDamage)
    {
        if(isInitialized)
            return;
        this.maxHp = maxHp;
        this.ATKDamage = ATKDamage;
        curHp = maxHp;

        isInitialized = true;
    }
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        box.isTrigger = true; 
        animator = GetComponentInChildren<Animator>();
    }
    public void OnWalk()
    {
        animator.SetBool("isMoving", true);
    }
    public void OnAttack()
    {
        animator.SetTrigger("attack");
    }
    public void Activate(Vector3 startPosition)
    {
        transform.position = startPosition;
        isActivated = true;
    }
    public void DestroySelf()
    {
        isActivated = false;
        Destroyed?.Invoke(this);
        curHp = maxHp;
    }
    protected IEnumerator KnockBack()
    {   
        isClosed = false;
        float elapsedTime=0f;
        while(elapsedTime < 0.1f)
        {
            this.transform.Translate(this.transform.right * 5 * Time.deltaTime);
            elapsedTime+=Time.deltaTime;
            yield return null;
        }
    }
    
    public abstract void Attacked(int damage);
}