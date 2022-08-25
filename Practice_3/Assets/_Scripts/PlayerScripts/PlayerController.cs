using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    Transform[] playerPosition;
    List<Player> playerList = new List<Player>();
    PlayerFactory playerFactory;
    PlayerSpawner playerSpawner;
    public Action<Unit, int> AttackEnemy;
    public Action<Player> NeedRespawn;
    public Action Dead;
    bool isInitialized = false;
    bool isMovingToNextWave = false;
    public void Initialize (PlayerSpawner playerSpawner, PlayerFactory playerFactory, Transform[] playerPosition)
    {
        if(isInitialized)
            return;
        this.playerSpawner = playerSpawner;
        this.playerFactory = playerFactory;
        this.playerPosition = playerPosition;

        playerList = playerSpawner.GetPlayerList;
        foreach(var player in playerList)
        {
            player.Destroyed += this.OnPlayerDestroyed;
            player.PlayerBattleStance += this.OnAttackEnemy;
        }
        Debug.Assert(playerList != null , "PlayerList is null!");
        isInitialized = true;
    }

    public void IsMoving(bool check)
    {
        if(check)
        {
            foreach(var player in playerList)
                player.OnWalk();
            isMovingToNextWave = true;
        }
        else
        {
            foreach(var player in playerList)
                player.OnIdle();
            isMovingToNextWave = false;
        }
    }
    void OnPlayerDestroyed(Unit unit) 
    {
        Dead?.Invoke();
        Player player = unit as Player;
        player.Destroyed -= this.OnPlayerDestroyed;
        player.PlayerBattleStance -= this.OnAttackEnemy;
        int index = playerList.IndexOf(player);
        playerList.RemoveAt(index);
        NeedRespawn?.Invoke(player);
        playerFactory.Restore(player, player.name);
    }
    public void RespawnPlayer(Player player)
    {
        playerList.Add(player);
        player.Destroyed += this.OnPlayerDestroyed;
        player.PlayerBattleStance += this.OnAttackEnemy;

        for(int i=0 ; i<playerList.Count ; i++)
        {
            StartCoroutine(SetPosition(playerList[i], playerPosition[i].position));
        }   
    }

    IEnumerator SetPosition(Player player, Vector3 target)
    {
        player.OnWalk();
        while(Vector3.Distance(player.transform.position, target) > 0.01f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, Time.deltaTime*1.5f);
            yield return null;
        }
        if(!isMovingToNextWave)
            player.OnIdle();
    }

    void OnAttackEnemy(Unit unit, int damage)
    {
        AttackEnemy?.Invoke(unit, damage);
    }
    public void OnAttacked(int damage)
    {
        foreach(var player in playerList)
        {
            if(player.GetActivate)
            {
                player.Attacked(damage);
                break;
            }
        }
    }
}
