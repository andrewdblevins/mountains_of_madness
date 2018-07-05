using System;
using System.Collections;
using System.Collections.Generic;

public class Player {

    private int health;
    private int attack;
    private int sanity;
    private int gold;

    private int dieSize = 6;

    private int maxHealth = 100;
    private int maxSanity = 100;

    private Random random = new Random();

    private void Initialize()
    {
        health = maxHealth;
        sanity = maxSanity;
        gold = 0;
        attack = 10;
    }

    public void ModifyHealth(int value)
    {
        health += value;
        health = Math.Max(health, maxHealth);
        if (health <= 0)
        {
            // TODO: die.
        }
    }

    public void ModifySanity(int value)
    {
        sanity += value;
        sanity = Math.Max(sanity, maxSanity);
    }

    public void ModifyAttack(int value)
    {
        attack += value;
    }

    public void ModifyGold(int value)
    {
        gold += value;
    }

    public void DoCombat(Monster monster)
    {
        ModifyHealth(-monster.GetAttack());
        ModifySanity(-monster.GetSanityAttack());
        monster.ModifyHealth(-(attack + random.Next(dieSize)));
    }

    public void AcquireTreasure(Treasure treasure)
    {
        ModifyGold(treasure.GetGold());
        ModifyHealth(treasure.GetHealth());
        ModifySanity(treasure.GetSanity());
        ModifyAttack(treasure.GetAttack());
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
