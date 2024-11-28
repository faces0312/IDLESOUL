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
        GoblinSpawn();
        GoblinSpawn();
        OrcSpawn();
    }
    private void InitializeEnemyPool()
    {
        ObjectPool goblinPool = new ObjectPool("Goblin", INITIAL_POOL_SIZE, "Prefabs/Enemy/Goblin");
        ObjectPool orcPool = new ObjectPool("Orc", INITIAL_POOL_SIZE, "Prefabs/Enemy/Orc");
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, orcPool);
    }


    public void GoblinSpawn()
    {
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_POOL_KEY, "Goblin");
        GameObject enemy = pool.GetObject();
        enemy.SetActive(true);
        Enemy tempEnemy = enemy.GetComponent<Enemy>();
    }

    public void OrcSpawn()
    {
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_POOL_KEY, "Orc");
        GameObject enemy = pool.GetObject();
        enemy.SetActive(true);
        Enemy tempEnemy = enemy.GetComponent<Enemy>();
    }
}
