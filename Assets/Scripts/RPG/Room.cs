using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour, IPointerClickHandler {
    public Image image;

    private Treasure treasure;
    private Monster monster;
    public Node node;

    //public RoomUI view;

    public void Initialize(Treasure treasure, Monster monster)
    {
        this.treasure = treasure;
        this.monster = monster;
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

    public void SetColor(Color color) {
        image.color = color;
    }

    public void OnPointerClick(PointerEventData eventData) {
        //AttemptEnter(new Player());
        Mountain.getInstance().MovePlayerTo(this);
    }
}
