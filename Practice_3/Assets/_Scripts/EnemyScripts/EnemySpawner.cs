using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemySpawner : MonoBehaviour
{
    EnemyDatabase enemyData;
    EnemyFactory enemyFactory;
    Unit[] enemyPrefabs;
    List<string> list = new List<string>();
    List<Enemy> enemys = new List<Enemy>();
    int maxWave = 3;
    int currentEnemyCount=0;
    int waveEnemyCount = 0;
    int currentWave=0;
    float waveInterval = 5f;
    float enemySpawnInterval = 0.5f;
    bool isInitialized = false;
    bool isSpawning = false;
    public Action EnemyDestroyed;
    public Action AllEnemyDestroyed; 
    public Action NextStage;
    public Action WaveEnd;
    public Action<bool> MovingToNextWave;
    public Action<int> WaveStarted;
    public Action<int> AttackPlayer;
    
    List<Enemy> enemies = new List<Enemy>();
    Dictionary<string, int> EnemyTypeCount = new Dictionary<string, int>();
    public void Initialize(EnemyDatabase enemyData, EnemyFactory enemyFactory, Unit[] enemyPrefabs)
    {
        if(isInitialized)
            return;
        this.enemyData = enemyData;
        this.enemyFactory = enemyFactory;
        this.enemyPrefabs = enemyPrefabs;

        Debug.Assert(this.enemyData != null, "EnemyDatabase is null!");
        Debug.Assert(this.enemyFactory != null, "EnemyFactory is null!");
        Debug.Assert(this.enemyPrefabs != null, "EnemyPrefabs is null!");

        isInitialized = true;
    }
    
    int CurWaveEnemyCount()
    {
        int enemyCount=0;
        EnemyTypeCount = enemyData.GetEnemyTypeCount(currentWave+1);
        foreach(var data in EnemyTypeCount)
        {
            enemyCount += data.Value;
        }
        return enemyCount;
    }
    public void Gamestart()
    {
        maxWave = enemyData.GetMaxWave;
        waveEnemyCount = CurWaveEnemyCount();
        currentEnemyCount = 0;
        StartCoroutine(AutoSpawnEnemy());
        MovingToNextWave?.Invoke(false);
        WaveStarted?.Invoke(currentWave+1);
    }

    
    IEnumerator AutoSpawnEnemy()
    {
        while(true) 
        {
            if(!isSpawning)
                StartCoroutine(SpawnEnemy());
            else
            {
                if(enemies.Count == 0)
                {
                    WaveEnd?.Invoke();
                    currentWave++;
                    if(currentWave >= maxWave)
                        yield break;
                    waveEnemyCount = CurWaveEnemyCount();
                    MovingToNextWave?.Invoke(true);
                    yield return new WaitForSeconds(waveInterval);
                    MovingToNextWave?.Invoke(false);
                    WaveStarted?.Invoke(currentWave+1);
                    currentEnemyCount = 0;
                    NextStage?.Invoke();
                    isSpawning = false;
                }
                else
                    yield return null;
            }
        }
    }
    IEnumerator SpawnEnemy()
    {
        Debug.Assert(this.enemyFactory != null, "enemy factory is null!");
        isSpawning = true;
        Unit unit = null;

        foreach(var data in EnemyTypeCount)
        {
            for(int i=0 ; i<data.Value ; i++)
            {
                unit = enemyFactory.GetUnit(enemyPrefabs,data.Key);
                foreach(var enemy in enemyData.GetEnemyStatData)
                {
                    if(data.Key == enemy.Name)
                        unit.Initialize(int.Parse(enemy.HP), int.Parse(enemy.ATK));
                }
                unit.Activate(new Vector3(5.5f, 0, 0));
                unit.OnWalk();

                Enemy temp = unit as Enemy;

                temp.Destroyed += this.OnEnemyDestroyed;
                temp.EnemyBattleStance += this.OnAttackPlayer;
                enemies.Add(temp);
                currentEnemyCount++;
                yield return new WaitForSeconds(enemySpawnInterval); 
            }
        }
    }
    
    public void OnEnemyAttacked(Unit unit, int damage)
    {
        int index = enemies.IndexOf(unit as Enemy);
        enemies[index].Attacked(damage);
    }
    void OnAttackPlayer(int damage)
    {
        AttackPlayer?.Invoke(damage);
        foreach(var enemy in enemies)
            enemy.StopMove();
    }
    void OnEnemyDestroyed(Unit unit) 
    {
        Enemy enemy = unit as Enemy;
        enemy.Destroyed -= this.OnEnemyDestroyed;
        enemy.EnemyBattleStance -= this.OnAttackPlayer;
        foreach(var obj in enemies)
            obj.StartMove();
        
        int index = enemies.IndexOf(enemy);
        enemies.RemoveAt(index); 
        enemyFactory.Restore(enemy, enemy.name);
        EnemyDestroyed?.Invoke();
        if (currentWave == maxWave-1 && enemies.Count == 0)
        {
            AllEnemyDestroyed?.Invoke();
        }
    }

    public void OnGameEnded(bool isVictory, int HeartCount)
    {
        if(enemies.Count == 0)
            return;
        
        foreach(var enemy in enemies)
        {
            enemyFactory.Restore(enemy, enemy.name);
        }
    }
}
