using System.Collections;
using System.Collections.Generic;

public class Treasure {

    private int gold;
    private int health;
    private int sanity;
    private int attack;
    private TreasureType treasureType;

    public enum TreasureType
    {
        ONE_SHOT,
        PERMANENT,
    }

    public void Initialize()
    {
        gold = 0;
        health = 0;
        sanity = 0;
        attack = 0;
        treasureType = TreasureType.ONE_SHOT;
    }

    public int GetGold()
    {
        return gold;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetSanity()
    {
        return sanity;
    }

    public int GetAttack()
    {
        return attack;
    }

    public TreasureType GetTreasureType()
    {
        return treasureType;
    }
}
