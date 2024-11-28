using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //ObjectPoolManager¿« Dictionary id
    private const string ENEMY_POOL_KEY = "Enemies";
    private const int INITIAL_POOL_SIZE = 10;

    private void Start()
    {
        InitializeEnemyPool();
        EnemySpawn(1, "Goblin", 5000);
        //EnemySpawn(2, "Orc", 5003);
    }
    private void InitializeEnemyPool()
    {
        ObjectPool goblinPool = new ObjectPool("Goblin", INITIAL_POOL_SIZE, "Prefabs/Enemy/Goblin");
        ObjectPool orcPool = new ObjectPool("Orc", INITIAL_POOL_SIZE, "Prefabs/Enemy/Orc");
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, orcPool);
    }

    public void EnemySpawn(int cycle, string name, int id)
    {
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_POOL_KEY, name);
        for (int i =0; i<cycle; i++)
        {
            GameObject enemy = pool.GetObject();
            Enemy tempEnemy = enemy.GetComponent<Enemy>();
            tempEnemy.enemyData = DataManager.Instance.EnemyDB[id];
            enemy.SetActive(true);
            GameManager.Instance.enemies.Add(enemy);
        }
    }
}
