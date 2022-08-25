using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    Player[] playerPrefabs;
    [SerializeField]
    TextAsset playerStatData;
    [SerializeField]
    Transform[] playerPosition;
    PlayerDatabase playerData;
    PlayerFactory playerFactory;
    PlayerSpawner playerSpawner;
    PlayerController playerController;
    SelectManager selectManager;
    List<Player> playerList = new List<Player>();
    public Action PlayerDead;
    public Action<Unit, int> PlayerAttack;
    void Awake()
    {
        selectManager = GameObject.Find("SelectManager").GetComponent<SelectManager>();
        playerData = new PlayerDatabase(playerStatData);
        playerFactory = new PlayerFactory();
        playerSpawner = new PlayerSpawner(playerData, playerFactory, playerPrefabs, playerPosition);
    }
    void Start()
    {
        playerSpawner.SetPlayerList(selectManager.GetCharacterList);
    }

    public void GameStart()
    {   
        playerList = playerSpawner.GetPlayerList;
        playerController = gameObject.AddComponent<PlayerController>();
        playerController.Initialize(playerSpawner, playerFactory, playerPosition);
        playerController.AttackEnemy += this.PlayerAttack;
        playerController.Dead += this.PlayerDead;
        playerController.NeedRespawn += playerSpawner.RespawnPlayer;
        playerSpawner.RespawnedPlayer += playerController.RespawnPlayer;
    }

    public void OnAttacked(int damage)
    {
        playerController.OnAttacked(damage);
    }
    public void IsMoving(bool check)
    {
        playerController.IsMoving(check);
    }
    void UnBindEvents()
    {
        playerController.AttackEnemy -= this.PlayerAttack;
        playerController.Dead -= this.PlayerDead;
        playerController.NeedRespawn -= playerSpawner.RespawnPlayer;
        playerSpawner.RespawnedPlayer -= playerController.RespawnPlayer;
    }
    void OnDestroy()
    {
        UnBindEvents();   
    }
}
