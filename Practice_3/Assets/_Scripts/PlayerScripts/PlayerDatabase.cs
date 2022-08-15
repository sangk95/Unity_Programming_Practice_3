using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStatDatabase
{
    public string Name, HP, ATK;
    public PlayerStatDatabase(string Name, string HP, string ATK)
    {
        this.Name = Name;
        this.HP = HP;
        this.ATK = ATK;
    }
}
public class PlayerDatabase
{
    TextAsset playerStatDatabase;
    List<PlayerStatDatabase> statDB = new List<PlayerStatDatabase>();

    public List<PlayerStatDatabase> GetPlayerStatData => statDB;
    public PlayerDatabase(TextAsset playerStatDatabase)
    {
        this.playerStatDatabase = playerStatDatabase;

        Debug.Assert(this.playerStatDatabase != null, "PlayerStatDatabase is null!");
        
        Setting();
    }
    void Setting()
    {
        string[] stat = playerStatDatabase.text.Substring(0, playerStatDatabase.text.Length-1).Split('\n');
        for(int i=1 ; i<stat.Length ; i++)
        {
            string[] row = stat[i].Split('\t');
            statDB.Add(new PlayerStatDatabase(row[0], row[1], row[2]));
        }
    }
}