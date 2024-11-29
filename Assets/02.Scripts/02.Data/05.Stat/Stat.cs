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

    public Stat()
    {

    }

    public Stat(Stat stat)
    {
        this.health = stat.health;
        this.maxHealth = stat.maxHealth;
        this.atk = stat.atk;
        this.def = stat.def;

        this.moveSpeed = stat.moveSpeed;
        this.atkSpeed = stat.atkSpeed;

        this.reduceDamage = stat.reduceDamage;

        this.critChance = stat.critChance;
        this.critDamage = stat.critDamage;
        this.coolDown = stat.coolDown;
    }
}
