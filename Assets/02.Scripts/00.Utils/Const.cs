//고정 상수,문자열을 저장하는 공간입니다.


public static class Const
{
    public static readonly string JsonItemDBPath = "JSON/ItemDB";
    public static readonly string JsonSellItemDBPath = "JSON/SellItemDB";
    public static readonly string JsonEnemyDBPath = "JSON/EnemyDB";
    public static readonly string JsonStageDBPath = "JSON/StageDB";
    public static readonly string JsonSoulDBPath = "JSON/SoulDB";
    public static readonly string JsonSkillDBPath = "JSON/SkillDB";
    public static readonly string JsonAchieveDBPath = "JSON/AchieveDB";
    public static readonly string JsonUserDBPath = "JSON/UserDB";
    public static readonly string JsonStatUpgradeDBPath = "JSON/StatUpgradeDB";
    public static readonly string JsonExchangeDBPath = "JSON/ExchangeDB";
    public static readonly string JsonGachaItemDBPath = "JSON/GachaItemDB";

    public static readonly string JsonUserDataPath = "/userdata.json";

    public static readonly int POOL_KEY_PLAYERPROJECTILE = 10; //Player 투사체 DB가 없기에 따로 Const에 저장됨
    public static readonly string PLAYER_PROJECTILE_ENERGYBOLT_PATH = "Prefabs/Player/Attack/EnergyBolt";
    public static readonly string PLAYER_PROJECTILE_ENERGYBOLT_KEY = "PlayerProjectile";
    public static readonly int PLAYER_INITIAL_POOL_SIZE = 60; //Player 투사체 DB가 없기에 따로 Const에 저장됨

    public static readonly string ENEMY_PREFEB_GOBLIN_PATH = "Prefabs/Enemy/Goblin";
    public static readonly string ENEMY_PREFEB_GOBLINMAGICIAN_PATH = "Prefabs/Enemy/GoblinMagician";
    public static readonly string ENEMY_PREFEB_SKELETON_PATH = "Prefabs/Enemy/Skeleton";
    public static readonly string ENEMY_PREFEB_SKELETONARCHER_PATH = "Prefabs/Enemy/SkeletonArcher";

    public static readonly string ENEMY_PREFEB_GOBLINBOSS_PATH = "Prefabs/Enemy/GoblinBoss";
    public static readonly string ENEMY_PREFEB_SKELETONBOSS_PATH = "Prefabs/Enemy/SkeletonBoss";

    public static readonly string ENEMY_PREFEB_GOBLINEENERGYBOLT_PATH = "Prefabs/Enemy/Effects/EnergyBolt";
    public static readonly string ENEMY_PREFEB_GOBLINSKILLBOSS1_PATH = "Prefabs/Enemy/Effects/SkillBoss1";
    public static readonly string ENEMY_PREFEB_SKELETONARROW_PATH = "Prefabs/Enemy/Effects/ArrowSkeleton";
    public static readonly string ENEMY_PREFEB_SKELETONENERGYBOLTBOSS_PATH = "Prefabs/Enemy/Effects/EnergyBoltBoss";
    public static readonly string ENEMY_PREFEB_SKELETONSKILLBOSS2_PATH = "Prefabs/Enemy/Effects/SkillBoss2";

    public static readonly string ENEMY_BOSS_POOL_KEY = "EnemyBoss";
    public static readonly string ENEMY_POOL_KEY = "Enemies";
    public static readonly string ENEMY_EFFECT_POOL_KEY = "EnemyEffect";

    public static readonly int DAMAGE_FONT_POOL_KEY = 20;
    public static readonly string DAMAGE_FONT_KEY = "DamageFont";
    public static readonly string DAMAGE_FONT_PATH = "Prefabs/UI/DamageFont";

    public static readonly string STAGE_CASTHLE_MAP_PATH = "Prefabs/Stage/CasthleStageMap";
    public static readonly string STAGE_FORESET_MAP_PATH = "Prefabs/Stage/ForestStageMap";

    public static readonly int INITIAL_POOL_SIZE = 60;

    public const int MAX_SOUL = 3;
}
  

