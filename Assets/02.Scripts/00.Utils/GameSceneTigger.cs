using UnityEngine;

public class GameSceneTigger : MonoBehaviour
{
    private const string ENEMY_BOSS_POOL_KEY = "EnemyBoss";
    private const string ENEMY_POOL_KEY = "Enemies";
    private const string ENEMY_EFFECT_POOL_KEY = "EnemyEffect";

    private void Start()
    {
        ObjectPoolSetting();

        GameManager.Instance.StartGame();
    }

    private void ObjectPoolSetting()
    {
        ObjectPoolManager.Instance.ObjectPoolAllClear();

        ObjectPool playerProjectilePool = new ObjectPool(Utils.POOL_KEY_PLAYERPROJECTILE, 60, "Prefabs/Player/Attack/EnergyBolt");
        ObjectPoolManager.Instance.AddPool("playerProjectile", playerProjectilePool);

        ObjectPool goblinPool = new ObjectPool(5000, 60, "Prefabs/Enemy/Goblin");
        ObjectPool goblinMagicianPool = new ObjectPool(5001, 60, "Prefabs/Enemy/GoblinMagician");

        ObjectPool slashPool = new ObjectPool(6000, 60, "Prefabs/Enemy/Effects/Slash");
        ObjectPool energyBoltPool = new ObjectPool(6001, 60, "Prefabs/Enemy/Effects/EnergyBolt");
        ObjectPool slashBossPool = new ObjectPool(6002, 60, "Prefabs/Enemy/Effects/SlashBoss");
        ObjectPool skillBoss1Pool = new ObjectPool(6003, 10, "Prefabs/Enemy/Effects/SkillBoss1");

        ObjectPool goblinBossPool = new ObjectPool(5500, 3, "Prefabs/Enemy/GoblinBoss");

        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_POOL_KEY, goblinMagicianPool);

        ObjectPoolManager.Instance.AddPool(ENEMY_EFFECT_POOL_KEY, slashPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_EFFECT_POOL_KEY, energyBoltPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_EFFECT_POOL_KEY, slashBossPool);
        ObjectPoolManager.Instance.AddPool(ENEMY_EFFECT_POOL_KEY, skillBoss1Pool);

        ObjectPoolManager.Instance.AddPool(ENEMY_BOSS_POOL_KEY, goblinBossPool);

    }
}