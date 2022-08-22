using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : Unit
{
    public Action<Unit, int> PlayerBattleStance;
    public override void Attacked(int damage)
    {
        curHp -= damage;
        if(curHp <= 0)
            DestroySelf();
    }
    IEnumerator PlayerAttack(Unit unit)
    {
        while(true)
        {
            if(!unit.GetActivate)
            {
                canAttack = true;
                elapsedTime = 0.5f;
                yield break;
            }
            if(elapsedTime < attackDelay)
                elapsedTime+=Time.deltaTime;
            else
            {
                OnAttack();
                canAttack = false;
                yield return new WaitForSeconds(0.3f);
                PlayerBattleStance?.Invoke(unit, ATKDamage);
                elapsedTime = 0;
            }
            yield return null;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isActivated)
            return;
        if(other.GetComponent<Enemy>() != null)
        {
            if(canAttack)
                StartCoroutine(PlayerAttack(other.GetComponent<Enemy>()));
            return;
        }
    }
}
