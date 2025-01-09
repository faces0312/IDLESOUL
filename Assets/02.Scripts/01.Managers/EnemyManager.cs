using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonDDOL<EnemyManager>
{
    [SerializeField] private bool isBoss;
    [SerializeField] private float spawnTime;
    private float spawnTimer;
    private int EnemyLimit = 100; //스테이지에 등장 할 수 있는 몬스터의 갯수 

    private List<Coroutine> enemySpawnCoroutines = new List<Coroutine>();

    private Dictionary<int, Enemy> enemyPrefabs = new Dictionary<int, Enemy>();

    [SerializeField] private float bossCameraCloseUpTime = 3.0f; // 보스 카메라 연출 지속시간

    public void Init()
    {
        InitializeEnemyPool();
        EnemySpawnStart();

        Debug.Log("EnemyManager 세팅 완료!!");
    }

    public void EnemySpawnStart()
    {
        List<int> summonEnenyIDList = StageManager.Instance.CurStageData.SummonEnemyIDList;

        if (GameManager.Instance.isGoldDungeon == false)
        {
            foreach (int ID in summonEnenyIDList)
            {
                //소환 갯수 , ID , 소환 주기
                enemySpawnCoroutines.Add(StartCoroutine(EnemySpawnCoroutine(ID, 3.0f)));
            }
        }
        else
            MimicSpawn();
    }

    public void EnemySpawnStop()
    {
        for (int i = 0; i < enemySpawnCoroutines.Count; i++)
        {
            StopCoroutine(enemySpawnCoroutines[i]);
            enemySpawnCoroutines[i] = null;
        }
        enemySpawnCoroutines.Clear();
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
        InitializeEnemyPrefab(5000, Const.ENEMY_PREFEB_GOBLIN_PATH);
        InitializeEnemyPrefab(5001, Const.ENEMY_PREFEB_GOBLINMAGICIAN_PATH);
        InitializeEnemyPrefab(5002, Const.ENEMY_PREFEB_SKELETON_PATH);
        InitializeEnemyPrefab(5003, Const.ENEMY_PREFEB_SKELETONARCHER_PATH);
        InitializeEnemyPrefab(5004, Const.ENEMY_PREFEB_WOLF_PATH);
        InitializeEnemyPrefab(5005, Const.ENEMY_PREFEB_WOLFGREEN_PATH);
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
        int maxAttempt = 10;
        int curAttaempt = 0;
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X, range_Z;

        BoxCollider SpawnArea = StageManager.Instance.CurStageMap.SpawnArea;
        float offsetX = SpawnArea.center.x, offsetZ = SpawnArea.center.z;

        Vector3 RandomPostion;
        do
        {
            curAttaempt++;
            range_X = UnityEngine.Random.Range((SpawnArea.size.x / 2) * -1, SpawnArea.size.x / 2);
            range_Z = UnityEngine.Random.Range((SpawnArea.size.z / 2) * -1, SpawnArea.size.z / 2);
            RandomPostion = new Vector3(range_X + offsetX, 1f, range_Z + offsetZ);
        }
        while (curAttaempt < maxAttempt && 4.0f >= Vector3.Distance(RandomPostion, playerPosition));

        Vector3 respawnPosition = RandomPostion;
        return respawnPosition;
    }

    IEnumerator EnemySpawnCoroutine( int id, float summonCoolTime, int cycle = 0)
    {
        yield return new WaitForSeconds(0.1f);
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(Const.ENEMY_POOL_KEY, id);

        Enemy prefabEnemy = GetInitializedEnemy(id);

        while (true)
        {
            if (GameManager.Instance.enemies.Count < EnemyLimit) // 몬스터가 120마리만 생성되게 세팅 
            {
                GameObject enemyObject = pool.GetObject();
                if (enemyObject.TryGetComponent(out RegularEnemy enemy))
                {
                    enemy.enemyDB = prefabEnemy.enemyDB;
                    enemy.Initialize();
                    enemy.target = GameManager.Instance.player.gameObject;
                }
                enemyObject.transform.position = RandomSpawn();
                enemyObject.SetActive(true);
                GameManager.Instance.enemies.Add(enemyObject);

                yield return new WaitForSeconds(summonCoolTime);

                //Test : 소환 주기가 점점 짧아지게 설정 ( 매직넘버 수정할것)
                if (summonCoolTime >= 0.7f)
                {
                    summonCoolTime *= 0.9f;
                }
            }
            else
            {
                yield return new WaitForSeconds(summonCoolTime);
            }
        }
    }
        
    public void BossSpawn(int id)
    {
        foreach (Coroutine spawnCoroutine in enemySpawnCoroutines)
        {
            StopCoroutine(spawnCoroutine);
        }

        GameManager.Instance.player.enabled = false;
        GameManager.Instance.IsBoss = true;
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

        ObjectPool pool = ObjectPoolManager.Instance.GetPool(Const.ENEMY_BOSS_POOL_KEY, id);
        GameObject enemyBoss = pool.GetObject();
        Enemy tempEnemy = enemyBoss.GetComponent<BossEnemy>();
        tempEnemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(id);
        tempEnemy.Initialize();
        tempEnemy.target = GameManager.Instance.player.gameObject;
        enemyBoss.transform.position = RandomSpawn();
        enemyBoss.SetActive(true);
        tempEnemy.BossAppear();
        GameManager.Instance.enemies.Add(enemyBoss);
        Debug.Log("보스 생성");

        UIManager.Instance.GetController<UIBossSummonAlarmController>().BossSummonAlarmView.BossNameSet(tempEnemy.enemyDB.Name);
        GameManager.Instance.cameraController.ToggleFollowTarget(tempEnemy.transform, bossCameraCloseUpTime);
    }

    public GameObject EnemyAttackSpawn(int id, Vector3 position, Quaternion rotation)
    {
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(Const.ENEMY_EFFECT_POOL_KEY, id);
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

    public void MimicSpawn()
    {
        foreach (Coroutine spawnCoroutine in enemySpawnCoroutines)
        {
            StopCoroutine(spawnCoroutine);
        }

        foreach (GameObject enemyTmp in GameManager.Instance.enemies)
        {
            if (enemyTmp != null)
            {
                enemyTmp.SetActive(false);
            }
        }

        GameManager.Instance.enemies.Clear();

        ObjectPool pool = ObjectPoolManager.Instance.GetPool(Const.ENEMY_BOSS_POOL_KEY, 5600);
        GameObject mimic = pool.GetObject();
        Enemy tempEnemy = mimic.GetComponent<BossEnemy>();
        tempEnemy.enemyDB = DataManager.Instance.EnemyDB.GetByKey(5600);
        tempEnemy.Initialize();
        tempEnemy.target = GameManager.Instance.player.gameObject;
        mimic.transform.position = new Vector3(0, 0, 3);
        mimic.SetActive(true);
        //tempEnemy.BossAppear();
        GameManager.Instance.enemies.Add(mimic);
/*
        UIManager.Instance.GetController<UIBossSummonAlarmController>().BossSummonAlarmView.BossNameSet(tempEnemy.enemyDB.Name);
        GameManager.Instance.cameraController.ToggleFollowTarget(tempEnemy.transform, bossCameraCloseUpTime);*/
    }
}
