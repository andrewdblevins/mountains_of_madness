using System.Collections;
using System.Collections.Generic;

public class Treasure {

    private int gold;
    private int health;
    private int sanity;
    private int attack;

    public void Initialize()
    {
        gold = 0;
        health = 0;
        sanity = 0;
        attack = 0;
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
