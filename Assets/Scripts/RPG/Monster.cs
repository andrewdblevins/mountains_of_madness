using System;
using System.Collections;
using System.Collections.Generic;

public class Monster {

    private int attack;
    private int health;
    private int dieSize;

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


    public void ModifyHealth(int value)
    {
        health += value;
        if (health <= 0)
        {
            // TODO: die.
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
