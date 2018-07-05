using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour, IPointerClickHandler {
    public Image image;

    private Treasure treasure;
    private Monster monster;
    private Node node;

    //public RoomUI view;

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

        SetColor(Color.green);
        foreach(Node n in node.neighbors) {
            n.room.SetColor(Color.yellow);
        }

        return true;
    }

    public void SetColor(Color color) {
        image.color = color;
    }

    public void OnPointerClick(PointerEventData eventData) {
        AttemptEnter(new Player());
    }
}
