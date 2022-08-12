using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyFactory enemyFactory;
    bool isInitialized = false;
    public void Initialize(EnemyFactory enemyFactory)
    {
        if(isInitialized)
            return;
        this.enemyFactory = enemyFactory;
        
        isInitialized = true;
    }
}
