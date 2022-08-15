using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    TextAsset enemySpawnData;
    [SerializeField]
    TextAsset enemyStatData;
    EnemyFactory enemyFactory;
    EnemyDatabase enemySpawner;
    
    void Awake()
    {
        enemyFactory = new EnemyFactory();
        enemySpawner = new EnemyDatabase(enemySpawnData, enemyStatData);
    }
}
