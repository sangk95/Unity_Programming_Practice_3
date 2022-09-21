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
    [SerializeField]
    UIRoot uIRoot;
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
        playerManager.PlayerDead += uIRoot.DeathCounting;
        enemyManager.EnemyAttack += playerManager.OnAttacked;
        enemyManager.WaveStarted += uIRoot.OnWaveChanged;
        enemyManager.IsMovingToNextWave += playerManager.IsMoving;
        enemyManager.GameEnded += uIRoot.OnGameEnded;
        foreach(var back in backGrounds)
            enemyManager.IsMovingToNextWave += back.checkMove;
        timeManager.GameStarted += playerManager.GameStart;
        timeManager.GameStarted += enemyManager.GameStart;
        timeManager.GameStarted += uIRoot.OnGameStarted;
    }

    void UnBindEvents()
    {
        playerManager.PlayerAttack -= enemyManager.OnAttacked;
        playerManager.PlayerDead -= uIRoot.DeathCounting;
        enemyManager.EnemyAttack -= playerManager.OnAttacked;
        enemyManager.WaveStarted -= uIRoot.OnWaveChanged;
        enemyManager.IsMovingToNextWave -= playerManager.IsMoving;
        enemyManager.GameEnded -= uIRoot.OnGameEnded;
        foreach(var back in backGrounds)
            enemyManager.IsMovingToNextWave -= back.checkMove;
        timeManager.GameStarted -= playerManager.GameStart;
        timeManager.GameStarted -= enemyManager.GameStart;
        timeManager.GameStarted -= uIRoot.OnGameStarted;
    }

    void OnDestroy()
    {
        UnBindEvents();   
    }

}
