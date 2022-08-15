using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager playerManager;
    TimeManager timeManager;
    void Awake()
    {
        timeManager = gameObject.AddComponent<TimeManager>();

        
        BindEvents();
        timeManager.StartGame();
    }

    void BindEvents()
    {
        timeManager.GameStarted += playerManager.GameStart;
    }

    void UnBindEvents()
    {
        timeManager.GameStarted -= playerManager.GameStart;
    }

    void OnDestroy()
    {
        UnBindEvents();   
    }
}
