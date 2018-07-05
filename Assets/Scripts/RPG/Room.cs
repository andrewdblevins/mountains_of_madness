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

    public void InitializeView() {
        if (node != null) {
            foreach (Node n in node.neighbors) {
                //LineRenderer line = gameObject.AddComponent<LineRenderer>() as LineRenderer;
                //line.SetPosition(0, transform.position);
                //line.SetPosition(1, n.room.transform.position);
                ////line.sortingOrder = 4;
                ////line.sortingLayerName = "UI";

                //line.sortingOrder = 1;
                //line.material = new Material(Shader.Find("Sprites/Default"));
                //line.material.color = Color.red;

                GameObject edge = Instantiate(image.gameObject, transform);
                edge.name = "Edge";
                edge.transform.position = (transform.position + n.room.transform.position) / 2f;
                float dist = Vector3.Distance(transform.position, n.room.transform.position);
                float deltaY = (transform.position.y - n.room.transform.position.y);
                deltaY = transform.position.x < n.room.transform.position.x ? -deltaY : deltaY;
                float angle = Mathf.Asin(deltaY / dist);
                Debug.Log(angle + " dist: " + dist + "  y: " + deltaY + "   sin: " + (deltaY / dist));
                //edge.transform.Rotate(Vector3.up, angle);
                edge.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Rad2Deg*angle);
                Canvas canvas = edge.gameObject.AddComponent<Canvas>() as Canvas;
                canvas.overrideSorting = true;
                (edge.transform as RectTransform).sizeDelta = new Vector2(dist, 10);
            }

        }
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
