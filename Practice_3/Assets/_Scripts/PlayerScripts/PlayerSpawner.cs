using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerSpawner
{
    PlayerDatabase playerData;
    PlayerFactory playerFactory;
    Player[] playerPrefabs;
    Transform[] playerPosition;
    List<string> list = new List<string>();
    List<Player> playerList = new List<Player>();
    public Action<Player> RespawnedPlayer;
    public List<Player> GetPlayerList => playerList;
    public PlayerSpawner(PlayerDatabase playerData, PlayerFactory playerFactory, Player[] playerPrefabs, Transform[] playerPosition)
    {
        this.playerData = playerData;
        this.playerFactory = playerFactory;
        this.playerPrefabs = playerPrefabs;
        this.playerPosition = playerPosition;

        Debug.Assert(this.playerData != null, "PlayerDatabase is null!");
        Debug.Assert(this.playerFactory != null, "PlayerFactory is null!");
        Debug.Assert(this.playerPrefabs != null, "PlayerPrefabs is null!");
        Debug.Assert(this.playerPosition != null, "PlayerPosition is null!");
    }

    public void GameStart()
    {
        foreach(var unit in playerList)
        {
            unit.OnWalk();
        }
    }
    
    public void SetPlayerList(List<string> list)
    {
        this.list = list;
        SpawnPlayer();
    }
    
    void SpawnPlayer()
    {
        Unit unit = null;
        int positionNum=0;
        for(int i=list.Count-1 ; i>=0 ; i--)
        {
            bool findUnit = false;
            foreach(var data in playerData.GetPlayerStatData)
            {
                if(data.Name == list[i])
                {
                    unit = playerFactory.GetUnit(playerPrefabs, list[i]);
                    unit.Initialize(int.Parse(data.HP), int.Parse(data.ATK));
                    
                    unit.Activate(playerPosition[positionNum].position);
                    
                    playerList.Add(unit as Player);
                    findUnit = true;
                }
            }
            if(findUnit)
                positionNum++;
        }
    }

    public void RespawnPlayer(Player player)
    {
        string reName = player.name.Replace("(Clone)","");
        Unit unit = null;
        unit = playerFactory.GetUnit(playerPrefabs, reName);
        foreach(var data in playerData.GetPlayerStatData)
            if(data.Name == reName)
                unit.Initialize(int.Parse(data.HP), int.Parse(data.ATK));
        unit.Activate(new Vector3(-5.5f,0,0));
        unit.OnWalk();
    
        RespawnedPlayer?.Invoke(unit as Player);
    }
}
