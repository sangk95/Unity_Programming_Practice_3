using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : UnitFactory
{
    Dictionary<string, List<Enemy>> pool = new Dictionary<string, List<Enemy>>();
    int defaultPoolSize;
    public override Unit CreateUnit(Unit[] unit, string name, int defaultPoolSize = 5)
    {
        this.defaultPoolSize = defaultPoolSize;
        Enemy enemy = null;
        switch(name)
        {
            case "Enemy_A":
                enemy =  CreateUnits(unit[0], name);
                break;
            case "Enemy_B":
                enemy = CreateUnits(unit[1], name);
                break;
            case "Enemy_C":
                enemy = CreateUnits(unit[2], name);
                break;
            default:
                break;
        }
        return enemy;
    }
    
    void CreatePool(Unit unit, string name)
    {
        List<Enemy> temp = new List<Enemy>();
        for(int i=0 ; i<defaultPoolSize ; i++)
        {
            Enemy obj = GameObject.Instantiate(unit) as Enemy;
            obj.gameObject.SetActive(false);
            temp.Add(obj);
        }
        pool.Add(name, temp);
    }

    public Enemy CreateUnits(Unit unit, string name)
    {
        if(!pool.ContainsKey(name) || pool[name].Count == 0)
        {
            CreatePool(unit, name);
        }
        int lastIndex = pool[name].Count-1;
        Enemy obj = pool[name][lastIndex];
        pool[name].RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Restore(Enemy unit, string name)
    {
        string reName = name.Replace("(Clone)","");
        Debug.Assert(unit != null, "Null object to be returned!");
        unit.gameObject.SetActive(false);
        pool[reName].Add(unit);
    }
}
