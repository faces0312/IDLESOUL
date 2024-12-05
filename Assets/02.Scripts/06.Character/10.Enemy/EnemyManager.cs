using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private bool isBoss;
    [SerializeField] private float spawnTime;
    private float spawnTimer;

    //ObjectPoolManager의 Dictionary id
    private const string ENEMY_BOSS_POOL_KEY = "EnemyBoss";
    private const string ENEMY_POOL_KEY = "Enemies";
    private const int INITIAL_POOL_SIZE = 10;

    //TestCode
    public BoxCollider SpawnArea;
    //

    private void Awake()
    {
        InitializeEnemyPool();
        StartCoroutine(EnemySpawnCoroutine(1, 5001));
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
        ObjectPool goblinPool = new ObjectPool(5000, INITIAL_POOL_SIZE, "Prefabs/Enemy/Goblin");
        ObjectPool goblinMagicianPool = new ObjectPool(5001, INITIAL_POOL_SIZE, "Prefabs/Enemy/GoblinMagician");

        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinMagicianPool);
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
            Enemy tempEnemy = enemy.GetComponent<RegularEnemy>();
            tempEnemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(id);
            //TestCode
            enemy.transform.position = RandomSpawn();
            //
            enemy.SetActive(true);
            GameManager.Instance.enemies.Add(enemy);

            yield return new WaitForSeconds(0.5f);
        }
    }
    //TODO :: 몬스터를 구분해서 특정 범위 몬스터들만 생성
    IEnumerator EnemyRandomSpawnCoroutine(int cycle)
    {
        for (int i = 0; i < cycle; i++)
        {
            int randomID = Random.Range(5000, 5009);
            ObjectPool pool = ObjectPoolManager.Instance.GetPool(ENEMY_POOL_KEY, randomID);
            GameObject enemy = pool.GetObject();
            Enemy tempEnemy = enemy.GetComponent<RegularEnemy>();
            tempEnemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(randomID);
            //TestCode
            enemy.transform.position = RandomSpawn();
            //
            enemy.SetActive(true);
            GameManager.Instance.enemies.Add(enemy);
            Debug.Log("몬스터 생성");
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Vector3 RandomSpawn()
    {
        int maxAttempt = 3;
        int curAttaempt = 0;
        Vector3 playerPosition = GameManager.Instance._player.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
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
        Enemy tempEnemy = enemyBoss.GetComponent<BossEnemy>();
        tempEnemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(5000);
        enemyBoss.SetActive(true);
        GameManager.Instance.enemies.Add(enemyBoss);
        Debug.Log("보스 생성");
    }
}
