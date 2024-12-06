using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private bool isBoss;
    [SerializeField] private float spawnTime;
    private float spawnTimer;


    //ObjectPoolManager�� Dictionary id
    private Dictionary<int, Enemy> enemyPrefabs = new Dictionary<int, Enemy>();
    private const string ENEMY_BOSS_POOL_KEY = "EnemyBoss";
    private const string ENEMY_POOL_KEY = "Enemies";
    private const string ENEMY_EFFECT_POOL_KEY = "EnemyEffect";
    private const int INITIAL_POOL_SIZE = 30;

    //TestCode
    public BoxCollider SpawnArea;
    //

    private void Start()
    {
        InitializeEnemyPool();
        BossSpawn(5000);
        //StartCoroutine(EnemySpawnCoroutine(1, 5000));
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
                //SpawnCycle();
                spawnTimer = spawnTime;
            }
        }
    }

    private void InitializeEnemyPool()
    {
        InitializeEnemyPrefab(5000, "Prefabs/Enemy/Goblin");
        InitializeEnemyPrefab(5001, "Prefabs/Enemy/GoblinMagician");

        ObjectPool goblinPool = new ObjectPool(5000, INITIAL_POOL_SIZE, "Prefabs/Enemy/Goblin");
        ObjectPool goblinMagicianPool = new ObjectPool(5001, INITIAL_POOL_SIZE, "Prefabs/Enemy/GoblinMagician");

        ObjectPool slashPool = new ObjectPool(6000, INITIAL_POOL_SIZE, "Prefabs/Enemy/Effects/Slash");
        ObjectPool energyBoltPool = new ObjectPool(6001, INITIAL_POOL_SIZE, "Prefabs/Enemy/Effects/EnergyBolt");
        ObjectPool slashBossPool = new ObjectPool(6002, INITIAL_POOL_SIZE, "Prefabs/Enemy/Effects/SlashBoss");

        ObjectPool goblinBossPool = new ObjectPool(5000, 3, "Prefabs/Enemy/GoblinBoss");

        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinMagicianPool);

        ObjectPoolManager.Instance.AddPool(ENEMY_EFFECT_POOL_KEY, slashPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_EFFECT_POOL_KEY, energyBoltPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_EFFECT_POOL_KEY, slashBossPool);

        ObjectPoolManager.Instance.AddPool(ENEMY_BOSS_POOL_KEY, goblinBossPool);
    }

    private void InitializeEnemyPrefab(int id, string prefabPath)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab != null)
        {
            RegularEnemy enemy = prefab.GetComponent<RegularEnemy>();
            if (enemy == null)
            {
                enemy = prefab.AddComponent<RegularEnemy>();
            }

            enemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(id);
            enemyPrefabs[id] = enemy;
        }
    }

    private Enemy GetInitializedEnemy(int id)
    {
        if (enemyPrefabs.TryGetValue(id, out Enemy prefab))
        {
            return prefab;
        }
        return null;
    }

    private Vector3 RandomSpawn()
    {
        int maxAttempt = 3;
        int curAttaempt = 0;
        Vector3 playerPosition = GameManager.Instance._player.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = SpawnArea.bounds.size.x;
        float range_Z = SpawnArea.bounds.size.z;

        Vector3 RandomPostion;
        do
        {
            curAttaempt++;
            range_X = UnityEngine.Random.Range((range_X / 2) * -1, range_X / 2);
            range_Z = UnityEngine.Random.Range((range_Z / 2) * -1, range_Z / 2);
            RandomPostion = new Vector3(range_X, 1f, range_Z);
        }
        while (curAttaempt < maxAttempt && 3.0f >= Vector3.Distance(RandomPostion, playerPosition));

        Vector3 respawnPosition =  RandomPostion;
        return respawnPosition;
    }

    IEnumerator EnemySpawnCoroutine(int cycle, int id)
    {
        yield return new WaitForSeconds(0.1f);
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_POOL_KEY, id);

        Enemy prefabEnemy = GetInitializedEnemy(id);
        for (int i = 0; i < cycle; i++)
        {
            GameObject enemyObject = pool.GetObject();
            if (enemyObject.TryGetComponent(out RegularEnemy enemy))
            {
                enemy.enemyDB = prefabEnemy.enemyDB;
            }

            enemyObject.transform.position = RandomSpawn();
            enemyObject.SetActive(true);
            GameManager.Instance.enemies.Add(enemyObject);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void BossSpawn(int id)
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

        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_BOSS_POOL_KEY, id);
        GameObject enemyBoss = pool.GetObject();
        Enemy tempEnemy = enemyBoss.GetComponent<BossEnemy>();
        tempEnemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(id);
        enemyBoss.transform.position = RandomSpawn();
        enemyBoss.SetActive(true);
        GameManager.Instance.enemies.Add(enemyBoss);
        Debug.Log("���� ����");
    }

    public GameObject EnemyAttackSpawn(int id, Vector3 position, Quaternion rotation)
    {
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_EFFECT_POOL_KEY, id);
        if (pool != null)
        {
            GameObject attackEffect = pool.GetObject();
            if (attackEffect != null)
            {
                attackEffect.transform.position = position;
                attackEffect.transform.rotation = rotation;
                attackEffect.SetActive(true);
                return attackEffect;
            }
        }
        return null;

    }
}
