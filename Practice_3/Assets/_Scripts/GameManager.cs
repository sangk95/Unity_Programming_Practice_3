using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    BackGround[] backGrounds;
    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    EnemyManager enemyManager;
    TimeManager timeManager;
    void Awake()
    {
        Application.targetFrameRate = 60;
        timeManager = gameObject.AddComponent<TimeManager>();

        BindEvents();
        timeManager.StartGame();
    }

    void BindEvents()
    {
        playerManager.PlayerAttack += enemyManager.OnAttacked;
        enemyManager.EnemyAttack += playerManager.OnAttacked;
        timeManager.GameStarted += playerManager.GameStart;
        timeManager.GameStarted += enemyManager.GameStart;
        foreach(var back in backGrounds)
        {
            timeManager.GameStarted += back.GameStart;
        }
    }

    void UnBindEvents()
    {
        playerManager.PlayerAttack -= enemyManager.OnAttacked;
        enemyManager.EnemyAttack -= playerManager.OnAttacked;
        timeManager.GameStarted -= playerManager.GameStart;
        timeManager.GameStarted -= enemyManager.GameStart;
        foreach(var back in backGrounds)
        {
            timeManager.GameStarted -= back.GameStart;
        }
    }

    void OnDestroy()
    {
        UnBindEvents();   
    }
}
