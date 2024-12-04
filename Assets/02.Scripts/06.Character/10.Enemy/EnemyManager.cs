using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private bool isBoss;
    [SerializeField] private float spawnTime;
    private float spawnTimer;

    //ObjectPoolManager�� Dictionary id
    private const string ENEMY_BOSS_POOL_KEY = "EnemyBoss";
    private const string ENEMY_POOL_KEY = "Enemies";
    private const int INITIAL_POOL_SIZE = 10;

    private void Start()
    {
        InitializeEnemyPool();
        StartCoroutine(EnemySpawnCoroutine(1, 5001));
        //StartCoroutine(EnemySpawnCoroutine(1,5006));
        //Invoke("SpawnCycle",1f);
        //EnemySpawn(1, "Goblin", 5000);
        //EnemySpawn(2, "Orc", 5003);
    }

    private void Update()
    {
        if (isBoss == false)
        {
            if (spawnTimer > 0)
            {
                spawnTimer -= Time.deltaTime;
            }
            else
            {
                SpawnCycle();
                spawnTimer = spawnTime;
            }
        }
    }

    private void InitializeEnemyPool()
    {
        InitializeEnemyPoolForType(5000, INITIAL_POOL_SIZE, "Prefabs/Enemy/Goblin");
        InitializeEnemyPoolForType(5001, INITIAL_POOL_SIZE, "Prefabs/Enemy/GoblinMagician");
        /*ObjectPool goblinPriestPool = new ObjectPool(5001, INITIAL_POOL_SIZE, "Prefabs/Enemy/GoblinPriest");
        ObjectPool goblinKingPool = new ObjectPool(5002, INITIAL_POOL_SIZE, "Prefabs/Enemy/GoblinKing");
        ObjectPool orcPool = new ObjectPool(5003, INITIAL_POOL_SIZE, "Prefabs/Enemy/Orc");
        ObjectPool orcWarriorPool = new ObjectPool(5004, INITIAL_POOL_SIZE, "Prefabs/Enemy/OrcWarrior");
        ObjectPool orcGuardPool = new ObjectPool(5005, INITIAL_POOL_SIZE, "Prefabs/Enemy/OrcGuard");
        ObjectPool batPool = new ObjectPool(5006, INITIAL_POOL_SIZE, "Prefabs/Enemy/Bat");
        ObjectPool spiderPool = new ObjectPool(5007, INITIAL_POOL_SIZE, "Prefabs/Enemy/Spider");
        ObjectPool snakePool = new ObjectPool(5008, INITIAL_POOL_SIZE, "Prefabs/Enemy/Snake");

        ObjectPool goblinBossPool = new ObjectPool(5000, 3, "Prefabs/Enemy/Goblin_Boss");
        ObjectPool batBossPool = new ObjectPool(5001, 3, "Prefabs/Enemy/Bat_Boss");*/

        /*ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinPriestPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinKingPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, orcPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, orcWarriorPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, orcGuardPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, batPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, spiderPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, snakePool);

        ObjectPoolManager.Instance.AddPool(ENEMY_BOSS_POOL_KEY, goblinBossPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_BOSS_POOL_KEY, batBossPool);*/
    }

    private void InitializeEnemyPoolForType(int id, int size, string prefabPath)
    {
        ObjectPool pool = new ObjectPool(id, size, prefabPath);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, pool);

        for (int i = 0; i < size; i++)
        {
            GameObject enemy = pool.GetObject();
            Enemy tempEnemy = enemy.GetComponent<Enemy>();
            if (tempEnemy != null)
            {
                tempEnemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(id);
            }
            enemy.SetActive(false); // ������Ʈ�� ��Ȱ��ȭ�Ͽ� Ǯ�� ��ȯ
        }
    }

    void SpawnCycle()
    {
        StartCoroutine(EnemyRandomSpawnCoroutine(5));
    }
    
    IEnumerator EnemySpawnCoroutine(int cycle, int id)
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_POOL_KEY, id);
        for (int i = 0; i < cycle; i++)
        {
            GameObject enemy = pool.GetObject();
            enemy.SetActive(true);
            GameManager.Instance.enemies.Add(enemy);

            yield return new WaitForSeconds(0.1f);
        }
    }
    //TODO :: ���͸� �����ؼ� Ư�� ���� ���͵鸸 ����
    IEnumerator EnemyRandomSpawnCoroutine(int cycle)
    {
        for (int i = 0; i < cycle; i++)
        {
            int randomID = Random.Range(5000, 5009);
            ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_POOL_KEY, randomID);
            GameObject enemy = pool.GetObject();
            enemy.SetActive(true);
            GameManager.Instance.enemies.Add(enemy);
            Debug.Log("���� ����");
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void BossSpawn()
    {
        isBoss = true;
        GameManager.Instance.isTryBoss = true;
        foreach (GameObject enemyTmp in GameManager.Instance.enemies)
        {
            if (enemyTmp != null)
            {
                enemyTmp.SetActive(false);
            }
        }
        GameManager.Instance.enemies.Clear();

        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_BOSS_POOL_KEY, 5000);
        GameObject enemyBoss = pool.GetObject();
        enemyBoss.SetActive(true);
        GameManager.Instance.enemies.Add(enemyBoss);
        Debug.Log("���� ����");
    }
}
