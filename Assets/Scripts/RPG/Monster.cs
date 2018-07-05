using System;
using System.Collections;
using System.Collections.Generic;

public class Monster {

    private int attack;
    private int health;
    private int dieSize;

    // Player acquires on kill.
    private Treasure treasure;

    private Random random = new Random();

    private void Initialize()
    {
        attack = 10;
        health = 10;
        dieSize = 6;
    }

    public int GetAttack()
    {
        return attack + random.Next(dieSize);
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
