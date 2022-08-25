using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : Unit
{
    public Action<int> EnemyBattleStance;
    public override void Attacked(int damage)
    {
        curHp -= damage;
        if(curHp <= 0)
            DestroySelf();
        else
            StartCoroutine(KnockBack());
    }
    IEnumerator EnemyAttack(Unit unit)
    {
        float elapsedTime = 0f;
        OnAttack();
        EnemyBattleStance?.Invoke(ATKDamage);
        if(!unit.GetActivate)
        {
            yield break;
        }
        while(true)
        {
            if(!isClosed)
                yield break;
            if(elapsedTime < attackDelay)
                elapsedTime+=Time.deltaTime;
            else
            {
                OnAttack();
                EnemyBattleStance?.Invoke(ATKDamage);
                elapsedTime = 0;
            }
            yield return null;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isActivated)
            return;
        if(other.GetComponent<Player>() != null)
        {
            isClosed = true;
            StartCoroutine(EnemyAttack(other.GetComponent<Player>()));
            return;
        }
    }
    public void StopMove()
    {
        isClosed = true;
        OnIdle();
    }
    public void StartMove()
    {
        isClosed = false;
        OnWalk();
    }
    void FixedUpdate()
    {
        if(!isActivated)
            return;
        if(!isClosed)
            transform.position += transform.right * -1.5f * Time.deltaTime; 
    }
}
