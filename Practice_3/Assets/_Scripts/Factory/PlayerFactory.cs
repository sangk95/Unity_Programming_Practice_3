using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : UnitFactory
{
    public override Unit CreateUnit(Unit[] unit, string name)
    {
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
    public void OnDestroy(Unit unit)
    {
        unit.gameObject.SetActive(false);
    }
}
