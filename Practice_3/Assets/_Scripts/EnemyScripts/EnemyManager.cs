using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    Unit[] enemyPrefabs;
    [SerializeField]
    TextAsset enemySpawnData;
    [SerializeField]
    TextAsset enemyStatData;
    EnemyFactory enemyFactory;
    EnemyDatabase enemyDatabase;
    EnemySpawner enemySpawner;
    public Action<int> EnemyAttack;
    void Awake()
    {
        enemyFactory = new EnemyFactory();
        //stageManager -> get cur_ stage
        enemyDatabase = new EnemyDatabase(1,enemySpawnData, enemyStatData);
        enemySpawner = gameObject.AddComponent<EnemySpawner>();
        enemySpawner.Initialize(enemyDatabase, enemyFactory, enemyPrefabs);
    }

    public void GameStart()
    {
        enemySpawner.Gamestart();
        enemySpawner.AttackPlayer += this.OnAttack;
    }
    void OnAttack(int damage)
    {
        EnemyAttack?.Invoke(damage);
    }
    public void OnAttacked(Unit unit, int damage)
    {
        enemySpawner.OnEnemyAttacked(unit, damage);
    }   

    void UnBindEvents()
    {
        enemySpawner.AttackPlayer -= this.OnAttack; 
    }
    void OnDestroy()
    {
        UnBindEvents();
    }
}
