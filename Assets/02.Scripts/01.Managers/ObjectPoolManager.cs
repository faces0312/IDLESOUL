using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonDDOL<ObjectPoolManager>
{
    private Dictionary<string, List<ObjectPool>> poolDict;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        poolDict = new Dictionary<string, List<ObjectPool>>();
        ObjectPoolAllClear();
        PlayerObjectPoolSetting();
        EnemyObjectPoolSetting();
        InventoryObjectPoolSetting();
        DamageFontObjectPoolSetting();
        Debug.Log("ObjectPoolManager Init 완료!!");
    }

    #region ObjectPoolInit
    private void InventoryObjectPoolSetting()
    {
    }

    private void PlayerObjectPoolSetting()
    {
        ObjectPool playerProjectilePool = new ObjectPool(Const.POOL_KEY_PLAYERPROJECTILE, Const.PLAYER_INITIAL_POOL_SIZE, Const.PLAYER_PROJECTILE_ENERGYBOLT_PATH);
        AddPool(Const.PLAYER_PROJECTILE_ENERGYBOLT_KEY, playerProjectilePool);
    }
    private void EnemyObjectPoolSetting()
    {
        ObjectPool goblinPool = new ObjectPool(5000, 100, Const.ENEMY_PREFEB_GOBLIN_PATH);
        ObjectPool goblinMagicianPool = new ObjectPool(5001, 100, Const.ENEMY_PREFEB_GOBLINMAGICIAN_PATH);
        ObjectPool skeletonPool = new ObjectPool(5002, 100, Const.ENEMY_PREFEB_SKELETON_PATH);
        ObjectPool skeletonArcherPool = new ObjectPool(5003, 100, Const.ENEMY_PREFEB_SKELETONARCHER_PATH);
        ObjectPool wolfPool = new ObjectPool(5004, 100, Const.ENEMY_PREFEB_WOLF_PATH);
        ObjectPool wolfGreenPool = new ObjectPool(5005, 100, Const.ENEMY_PREFEB_WOLFGREEN_PATH);

        ObjectPool energyBoltPool = new ObjectPool(6001, 60, Const.ENEMY_PREFEB_GOBLINEENERGYBOLT_PATH);
        ObjectPool skillBoss1Pool = new ObjectPool(6003, 10, Const.ENEMY_PREFEB_GOBLINSKILLBOSS1_PATH);
        ObjectPool arrowSkeletonPool = new ObjectPool(6005, 100, Const.ENEMY_PREFEB_SKELETONARROW_PATH);
        ObjectPool energyBoltBOSSPool = new ObjectPool(6006, 100, Const.ENEMY_PREFEB_SKELETONENERGYBOLTBOSS_PATH);
        ObjectPool skillBoss2Pool = new ObjectPool(6007, 10, Const.ENEMY_PREFEB_SKELETONSKILLBOSS2_PATH);
        ObjectPool mimicAttack = new ObjectPool(6008, 25, Const.ENEMY_PREFEB_MIMICATTACK_PATH);
        ObjectPool wolfRange = new ObjectPool(6009, 25, Const.ENEMY_PREFEB_WOLFRANGE_PATH);

        ObjectPool goblinBossPool = new ObjectPool(5500, 1, Const.ENEMY_PREFEB_GOBLINBOSS_PATH);
        ObjectPool skeletonBossPool = new ObjectPool(5501, 1, Const.ENEMY_PREFEB_SKELETONBOSS_PATH);
        ObjectPool wolfBossPool = new ObjectPool(5502, 1, Const.ENEMY_PREFEB_WOLFBOSS_PATH);

        ObjectPool mimicPool = new ObjectPool(5600, 1, Const.ENEMY_PREFEB_Mimic_PATH);

        AddPool(Const.ENEMY_POOL_KEY, goblinPool);
        AddPool(Const.ENEMY_POOL_KEY, goblinMagicianPool);
        AddPool(Const.ENEMY_POOL_KEY, skeletonPool);
        AddPool(Const.ENEMY_POOL_KEY, skeletonArcherPool);
        AddPool(Const.ENEMY_POOL_KEY, wolfPool);
        AddPool(Const.ENEMY_POOL_KEY, wolfGreenPool);

        AddPool(Const.ENEMY_EFFECT_POOL_KEY, energyBoltPool);
        AddPool(Const.ENEMY_EFFECT_POOL_KEY, skillBoss1Pool);
        AddPool(Const.ENEMY_EFFECT_POOL_KEY, arrowSkeletonPool);
        AddPool(Const.ENEMY_EFFECT_POOL_KEY, energyBoltBOSSPool);
        AddPool(Const.ENEMY_EFFECT_POOL_KEY, skillBoss2Pool);
        AddPool(Const.ENEMY_EFFECT_POOL_KEY, mimicAttack);
        AddPool(Const.ENEMY_EFFECT_POOL_KEY, wolfRange);

        AddPool(Const.ENEMY_BOSS_POOL_KEY, goblinBossPool);
        AddPool(Const.ENEMY_BOSS_POOL_KEY, skeletonBossPool);
        AddPool(Const.ENEMY_BOSS_POOL_KEY, wolfBossPool);

        AddPool(Const.ENEMY_BOSS_POOL_KEY, mimicPool);
    }

    private void DamageFontObjectPoolSetting()
    {
        ObjectPool dmgFontPool = new ObjectPool(Const.DAMAGE_FONT_POOL_KEY, Const.INITIAL_POOL_SIZE, Const.DAMAGE_FONT_PATH);
        ObjectPoolManager.Instance.AddPool(Const.DAMAGE_FONT_KEY, dmgFontPool);

        ObjectPool audioSourcePool = new ObjectPool(Const.AUDIO_SOURCE_POOL_KEY, Const.INITIAL_POOL_SIZE, Const.AUDIO_SOURCE_PATH);
        ObjectPoolManager.Instance.AddPool(Const.AUDIO_SOURCE_KEY, audioSourcePool);
    }

    #endregion

    /// <summary>
    /// 오브젝트 풀 딕셔너리 내부의 풀 리스트를 반환
    /// </summary>
    /// <param name="id">딕셔너리 key</param>
    /// <returns>List<Pool></returns>
    public List<ObjectPool> GetPoolList(string id)
    {
        return poolDict[id];
    }
    /// <summary>
    /// 오브젝트 풀 딕셔너리 내부 풀 리스트에서 풀을 반환
    /// </summary>
    /// <param name="dictId">딕셔너리 key</param>
    /// <param name="poolId">오브젝트 풀 ID</param>
    /// <returns>ObjectPool</returns>
    public ObjectPool GetPool(string dictId, int poolId)
    {
        if (poolDict.ContainsKey(dictId))
        {
            for (int i = 0; i < poolDict[dictId].Count; i++)
            {
                if (poolDict[dictId][i].Id == poolId)
                    return poolDict[dictId][i];
            }
        }
        return null;
    }
    /// <summary>
    /// 딕셔너리[id]에 풀을 추가, 없다면 생성
    /// </summary>
    /// <param name="id">딕셔너리 키</param>
    /// <param name="pool">추가할 오브젝트 풀</param>
    public void AddPool(string id, ObjectPool pool)
    {
        if (!poolDict.ContainsKey(id))
        {
            List<ObjectPool> objPool = new List<ObjectPool>();
            objPool.Add(pool);
            poolDict.Add(id, objPool);
        }
        else
        {
            poolDict[id].Add(pool);
        }
    }

    /// <summary>
    /// 딕셔너리[id]에 풀을 추가, 없다면 생성 - 제네릭으로 수정 중 
    /// </summary>
    /// <param name="id">딕셔너리 키</param>
    /// <param name="pool">추가할 오브젝트 풀</param>
    public void AddPool<T>(ObjectPool pool)
    {
        string id = typeof(T).ToString();

        if (!poolDict.ContainsKey(id))
        {
            List<ObjectPool> objPool = new List<ObjectPool>();
            objPool.Add(pool);
            poolDict.Add(id, objPool);
        }
        else
        {
            poolDict[id].Add(pool);
        }
    }

    private void ObjectPoolAllClear()
    {
        foreach(KeyValuePair<string, List<ObjectPool>> pool in poolDict)
        {
            pool.Value.Clear();
        }
    }

    public void ObjectPoolAllReturn(string id)
    {
        if (poolDict.ContainsKey(id))
        {
            foreach (ObjectPool objPool in poolDict[id])
            {
                objPool.SetActiveAllFalse();
            }
        }
    }
    /* 
    /// <summary>
    /// 오브젝트 풀 딕셔너리 내부의 풀 리스트를 반환
    /// </summary>
    /// <param name="id">딕셔너리 key</param>
    /// <returns>List<Pool></returns>
    public List<ObjectPool> GetPoolList(int id)
    {
        return poolDict[id];
    }


    /// <summary>
    /// 오브젝트 풀 딕셔너리 내부 풀 리스트에서 풀을 반환
    /// </summary>
    /// <param name="dictId">딕셔너리 key</param>
    /// <param name="poolId">오브젝트 풀 ID</param>
    /// <returns>ObjectPool</returns>
    public ObjectPool GetPool(int dictId, int poolId)
    {
        for (int i = 0; i < poolDict[dictId].Count; i++)
        {
            if (poolDict[dictId][i].Id == poolId)
                return poolDict[dictId][i];
        }
        return null;
    }

    /// <summary>
    /// 딕셔너리[id]에 풀을 추가, 있다면 갱신, 없다면 생성
    /// </summary>
    /// <param name="dictId">딕셔너리 키</param>
    /// <param name="pool">추가할 오브젝트 풀</param>
    public void AddPool(int id, ObjectPool pool)
    {
        if (!poolDict.ContainsKey(dictId))
        {
            List<ObjectPool> objPool = new List<ObjectPool>();
            objPool.Add(pool);
            poolDict.Add(dictId, objPool);
        }
        else if(poolDict[dictId].Contains(pool))
        {
            poolDict[dictId].Remove(pool);
            poolDict[dictId].Add(pool);
        }
        else
        {
            poolDict[dictId].Add(pool);
        }
    }
    */
}
