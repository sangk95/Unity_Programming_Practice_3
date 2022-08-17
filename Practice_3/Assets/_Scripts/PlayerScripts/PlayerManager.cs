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
    SelectManager selectManager;
    void Awake()
    {
        selectManager = GameObject.Find("SelectManager").GetComponent<SelectManager>();
        playerData = new PlayerDatabase(playerStatData);
        playerFactory = new PlayerFactory();
        playerSpawner = new PlayerSpawner(playerData, playerFactory, playerPrefabs);
    }
    void Start()
    {
        playerSpawner.SetPlayerList(selectManager.GetCharacterList);
    }

    public void GameStart()
    {   
        playerSpawner.GameStart();
    }
}
