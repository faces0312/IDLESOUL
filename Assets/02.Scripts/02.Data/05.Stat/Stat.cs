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
    public BigInteger totalDamage;

    public int iD;

    public BigInteger health;
    public BigInteger maxHealth;
    public BigInteger atk;
    public BigInteger def;

    public float moveSpeed;
    public float atkSpeed;

    public float reduceDamage;

    public float critChance;
    public float critDamage;
    public float coolDown;
}
