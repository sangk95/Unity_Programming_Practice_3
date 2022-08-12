using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public override void Attacked(int damage)
    {
        curHp -= damage;
        if(curHp <= 0)
            DestroySelf();
    }
}
