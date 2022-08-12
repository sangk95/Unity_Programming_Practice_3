using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : UnitFactory
{
    int defaultPoolSize;
    public override Unit CreateUnit(Unit[] unit, string name, int defaultPoolSize = 1)
    {
        this.defaultPoolSize = defaultPoolSize;
        Unit temp = null;
        foreach(var obj in unit)
        {
            if(obj.name == name)
            {
                temp = obj;
                break;
            }
        }
        Player player = GameObject.Instantiate(temp) as Player;
        return player;
    }
}
