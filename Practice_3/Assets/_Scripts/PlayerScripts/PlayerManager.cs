using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    Unit[] playerPrefabs;
    [SerializeField]
    TextAsset playerStatData;
    PlayerDatabase playerData;
    PlayerFactory playerFactory;
    PlayerSpawner playerSpawner;
    void Awake()
    {
        playerData = new PlayerDatabase(playerStatData);
        playerFactory = new PlayerFactory();
        playerSpawner = new PlayerSpawner(playerData, playerFactory, playerPrefabs);
    }
    void Start()
    {
        //------------------------------Select Player를 통해 리스트 전달하기로 변경--------------------------//
        List<string> list = new List<string>();
        list.Add("Player01");
        list.Add("Player02");
        list.Add("Player03");
        playerSpawner.SetPlayerList(list);
    }

    public void GameStart()
    {   
        playerSpawner.GameStart();
    }
}
