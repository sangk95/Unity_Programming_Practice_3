using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : UnitFactory
{
    Dictionary<string, List<Player>> pool = new Dictionary<string, List<Player>>();
    int defaultPoolSize = 5;
    public override Unit CreateUnit(Unit[] unit, string name)
    {
        Player player = null;
        Unit temp = null;
        foreach(var obj in unit)
        {
            if(obj.name == name)
            {
                temp = obj;
                break;
            }
        }
        player =  CreateUnits(temp, name);
        return player;
    }
    
    void CreatePool(Unit unit, string name)
    {
        List<Player> temp = new List<Player>();
        for(int i=0 ; i<defaultPoolSize ; i++)
        {
            Player obj = GameObject.Instantiate(unit) as Player;
            obj.gameObject.SetActive(false);
            temp.Add(obj);
        }
        pool.Add(name, temp);
    }

    public Player CreateUnits(Unit unit, string name)
    {
        if(!pool.ContainsKey(name) || pool[name].Count == 0)
        {
            CreatePool(unit, name);
        }
        int lastIndex = pool[name].Count-1;
        Player obj = pool[name][lastIndex];
        pool[name].RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Restore(Player unit, string name)
    {
        string reName = name.Replace("(Clone)","");
        Debug.Assert(unit != null, "Null object to be returned!");
        unit.gameObject.SetActive(false);
        pool[reName].Add(unit);
    }
}
