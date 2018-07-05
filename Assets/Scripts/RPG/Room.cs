using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour, IPointerClickHandler {
    public Image image;

    private List<GameObject> edges = new List<GameObject>();

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
            int count = 0;
            foreach (Node n in node.neighbors) {
                count++;
                if (edges.Count < count) {
                    GameObject edge = Instantiate(image.gameObject, transform);
                    edge.name = "Edge";
                    Canvas canvas = edge.AddComponent<Canvas>() as Canvas;
                    SetEdge(edge, n);

                    edges.Add(edge);
                }
            }

        }
    }

    public void Update() {
        int i = 0;
        foreach(GameObject edge in edges) {
            
            if (node != null && node.neighbors!= null && node.neighbors.Count > i) {
                SetEdge(edge, node.neighbors[i]);
            }

            i++;
        }
    }

    private void SetEdge(GameObject edge, Node other) {
        edge.transform.position = (transform.position + other.room.transform.position) / 2f;
        float dist = Vector3.Distance(transform.position, other.room.transform.position);
        float deltaY = (transform.position.y - other.room.transform.position.y);
        deltaY = transform.position.x < other.room.transform.position.x ? -deltaY : deltaY;
        float angle = Mathf.Asin(deltaY / dist);
        //Debug.Log(angle + " dist: " + dist + "  y: " + deltaY + "   sin: " + (deltaY / dist));
        //edge.transform.Rotate(Vector3.up, angle);
        edge.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Rad2Deg * angle);
        Canvas canvas = edge.GetComponent<Canvas>() as Canvas;
        canvas.overrideSorting = true;
        (edge.transform as RectTransform).sizeDelta = new Vector2(dist, 10);
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
