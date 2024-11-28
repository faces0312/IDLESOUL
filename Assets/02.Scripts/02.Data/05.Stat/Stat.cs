using ScottGarland;

public enum StatType
{
    Player,
    Soul,
    Enemy,
}

[System.Serializable]
public class Stat
{
    public BigInteger combatPower;

    public int iD;
    public int level;

    public BigInteger health;
    public BigInteger maxHealth;
    public BigInteger atk;
    public BigInteger def;

    public float reduceDamage;
    public float criticalRate;
    public float criticalDamage;

    public float atkSpeed;
    public float moveSpeed;
    public float coolDown;

    public BigInteger exp;
    public BigInteger MaxExp;
}
