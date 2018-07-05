using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster {

    private int attack;
    private int sanityAttack;
    private int health;
    private int dieSize;
    private int sanityDieSize;

    // Player acquires on kill.
    private Treasure treasure;

    public Monster(int attack, int sanityAttack, int health, int dieSize, int sanityDieSize, Treasure treasure)
    {
        this.attack = attack;
        this.sanityAttack = sanityAttack;
        this.health = health;
        this.dieSize = dieSize;
        this.sanityDieSize = sanityDieSize;
        this.treasure = treasure;
    }

    public int GetAttack()
    {
        return attack + Random.Range(1, dieSize);
    }

    public int GetSanityAttack()
    {
        return sanityAttack + Random.Range(1, sanityDieSize);
    }

    public Treasure GetTreasure()
    {
        return treasure;
    }

    public void ModifyHealth(int value)
    {
        health += value;
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
