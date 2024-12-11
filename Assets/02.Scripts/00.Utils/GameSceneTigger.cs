using UnityEngine;

public class GameSceneTigger : MonoBehaviour
{


    private void Start()
    {
        ObjectPoolManager.Instance.ObjectPoolAllClear();

        PlayerObjectPoolSetting();
        EnemyObjectPoolSetting();

        GameManager.Instance.StartGame();
    }

    private void PlayerObjectPoolSetting()
    {
        ObjectPool playerProjectilePool = new ObjectPool(Const.POOL_KEY_PLAYERPROJECTILE, Const.PLAYER_INITIAL_POOL_SIZE, Const.PLAYER_PROJECTILE_ENERGYBOLT_PATH);
        ObjectPoolManager.Instance.AddPool(Const.PLAYER_PROJECTILE_ENERGYBOLT_KEY, playerProjectilePool);
    }

    private void EnemyObjectPoolSetting()
    {
        ObjectPool goblinPool = new ObjectPool(5000, 60, Const.ENEMY_PREFEB_GOBLIN_PATH);
        ObjectPool goblinMagicianPool = new ObjectPool(5001, 60, Const.ENEMY_PREFEB_GOBLINMAGICIAN_PATH);

        ObjectPool slashPool = new ObjectPool(6000, 60, Const.ENEMY_PREFEB_GOBLINSLASH_PATH);
        ObjectPool energyBoltPool = new ObjectPool(6001, 60, Const.ENEMY_PREFEB_GOBLINEENERGYBOLT_PATH);
        ObjectPool slashBossPool = new ObjectPool(6002, 60, Const.ENEMY_PREFEB_GOBLINSLASHBOSS_PATH);
        ObjectPool skillBoss1Pool = new ObjectPool(6003, 10, Const.ENEMY_PREFEB_GOBLINSKILLBOSS1_PATH);

        ObjectPool goblinBossPool = new ObjectPool(5500, 3, Const.ENEMY_PREFEB_GOBLINBOSS_PATH);

        ObjectPoolManager.Instance.AddPool(Const.ENEMY_POOL_KEY, goblinPool);
        ObjectPoolManager.Instance.AddPool(Const.ENEMY_POOL_KEY, goblinMagicianPool);

        ObjectPoolManager.Instance.AddPool(Const.ENEMY_EFFECT_POOL_KEY, slashPool);
        ObjectPoolManager.Instance.AddPool(Const.ENEMY_EFFECT_POOL_KEY, energyBoltPool);
        ObjectPoolManager.Instance.AddPool(Const.ENEMY_EFFECT_POOL_KEY, slashBossPool);
        ObjectPoolManager.Instance.AddPool(Const.ENEMY_EFFECT_POOL_KEY, skillBoss1Pool);

        ObjectPoolManager.Instance.AddPool(Const.ENEMY_BOSS_POOL_KEY, goblinBossPool);
    }
}