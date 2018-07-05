using System.Collections;

public class Room 
{
    private Treasure treasure;
    private Monster monster;

    public void Initialize()
    {
        // set up monster/treasure;
    }

    public bool AttemptEnter(Player player)
    {
        if (monster != null)
        {
            player.DoCombat(monster);
            if (monster.IsDead())
            {
                Treasure monsterTreasure = monster.GetTreasure();
                if (monsterTreasure != null)
                {
                    player.AcquireTreasure(monsterTreasure);
                }
                // Notify player?
                monster = null;
            } else
            {
                return false;
            }
        }
        if (treasure != null)
        {
            // Notify player?
            player.AcquireTreasure(treasure);
            if (treasure.GetTreasureType() == Treasure.TreasureType.ONE_SHOT)
            {
                treasure = null;
            }
        }
        return true;
    }
}
