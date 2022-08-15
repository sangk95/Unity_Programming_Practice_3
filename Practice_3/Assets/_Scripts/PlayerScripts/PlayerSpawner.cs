using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner
{
    PlayerDatabase playerData;
    PlayerFactory playerFactory;
    Unit[] playerPrefabs;
    List<string> list = new List<string>();
    List<Unit> players = new List<Unit>();
    public PlayerSpawner(PlayerDatabase playerData, PlayerFactory playerFactory, Unit[] playerPrefabs)
    {
        this.playerData = playerData;
        this.playerFactory = playerFactory;
        this.playerPrefabs = playerPrefabs;

        Debug.Assert(this.playerData != null, "PlayerDatabase is null!");
        Debug.Assert(this.playerFactory != null, "PlayerFactory is null!");
        Debug.Assert(this.playerPrefabs != null, "PlayerPrefabs is null!");
    }

    public void GameStart()
    {
        SpawnPlayer();
    }
    
    public void SetPlayerList(List<string> list)
    {
        this.list = list;
    }
    
    void SpawnPlayer()
    {
        Unit unit = null;
        for(int i=0 ; i<list.Count ; i++)
        {
            unit = playerFactory.GetUnit(playerPrefabs, list[i]);
            foreach(var data in playerData.GetPlayerStatData)
            {
                if(data.Name == list[i])
                {
                    unit.Initialize(int.Parse(data.HP), int.Parse(data.ATK));
                    
                    unit.Activate(new Vector3(-1-i, 0, 0));
                    unit.OnWalk();

                    unit.Destroyed += this.OnPlayerDestroyed;
                    players.Add(unit);
                }
            }
        }
    }

    void OnPlayerDestroyed(Unit unit) 
    {
        unit.Destroyed -= this.OnPlayerDestroyed;
        int index = players.IndexOf(unit);
        players.RemoveAt(index);
        playerFactory.OnDestroy(unit);
    }

}
