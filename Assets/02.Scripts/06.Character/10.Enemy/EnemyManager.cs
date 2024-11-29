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

        Invoke("SpawnCycle",1f);
        //EnemySpawn(1, "Goblin", 5000);
        //EnemySpawn(2, "Orc", 5003);
    }
    private void InitializeEnemyPool()
    {
        ObjectPool goblinPool = new ObjectPool(5000, INITIAL_POOL_SIZE, "Prefabs/Enemy/Goblin");
        /*ObjectPool goblinPriestPool = new ObjectPool("GoblinPriest", INITIAL_POOL_SIZE, "Prefabs/Enemy/GoblinPriest");
        ObjectPool goblinKingPool = new ObjectPool("GoblinKing", INITIAL_POOL_SIZE, "Prefabs/Enemy/GoblinKing");
        ObjectPool orcPool = new ObjectPool("Orc", INITIAL_POOL_SIZE, "Prefabs/Enemy/Orc");
        ObjectPool orcWarriorPool = new ObjectPool("OrcWarrior", INITIAL_POOL_SIZE, "Prefabs/Enemy/OrcWarrior");
        ObjectPool orcGuardPool = new ObjectPool("OrcGuard", INITIAL_POOL_SIZE, "Prefabs/Enemy/OrcGuard");
        ObjectPool batPool = new ObjectPool("Bat", INITIAL_POOL_SIZE, "Prefabs/Enemy/Bat");
        ObjectPool spiderPool = new ObjectPool("Spider", INITIAL_POOL_SIZE, "Prefabs/Enemy/Spider");
        ObjectPool snakePool = new ObjectPool("Snake", INITIAL_POOL_SIZE, "Prefabs/Enemy/Snake");*/
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinPool);
        //ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, orcPool);
    }
    void SpawnCycle()
    {
        EnemySpawn(1, 5000);
    }

    public void EnemySpawn(int cycle, int id)
    {
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_POOL_KEY, id);
        for (int i =0; i<cycle; i++)
        {
            GameObject enemy = pool.GetObject();
            Enemy tempEnemy = enemy.GetComponent<Enemy>();
            tempEnemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(id);
            enemy.SetActive(true);
            GameManager.Instance.enemies.Add(enemy);
        }
    }
}
