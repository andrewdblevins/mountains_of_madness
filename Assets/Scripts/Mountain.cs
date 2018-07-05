using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mountain : MonoBehaviour {

    private static Mountain instance = null;

    public static Mountain getInstance() {
        return instance;
    }

    public List<Room> rooms = new List<Room>();
    public bool button = false;
    public bool start = false;
    public GameObject roomPrefab;
    public Canvas canvas;

    private Player player;
    private Room currentRoom;

    public void Generate( List<Node> graph) {
        foreach( Node n in graph) {
            GameObject roomObj = Instantiate(roomPrefab, canvas.transform);
            roomObj.transform.localPosition = new Vector3(Random.Range(-250f, 250f), Random.Range(-200f, 200f), 0f);
            Room room = roomObj.GetComponent<Room>();
            room.node = n;
            n.room = room;
            rooms.Add(room);
        }
    }

    //public void AddRoom( Room room) {
    //    rooms.Add(room);
    //    GameObject roomObj = Instantiate(roomPrefab, canvas.transform);
    //    roomObj.transform.localPosition = new Vector3(Random.Range(-250f, 250f), Random.Range(-200f, 200f), 0f);
    //    //room.view = roomObj.GetComponent<RoomUI>();
    //}

    public void Update() {
        if (button) {
            button = false;

            Node A = new Node();
            Node B = new Node();
            Node C = new Node();
            A.neighbors.Add(B);
            B.neighbors.Add(C);
            B.neighbors.Add(A);
            C.neighbors.Add(B);

            List<Node> graph = new List<Node>();
            graph.Add(A);
            graph.Add(B);
            graph.Add(C);

            Generate(graph);
        }
        if (start) {
            start = false;

            player = new Player();
        }
    }

    public void Awake() {
        instance = this;
    }

    public bool MovePlayerTo(Room room) {
        if(currentRoom != null && !currentRoom.node.neighbors.Contains(room.node)) {
            return false;
        }

        bool success = room.AttemptEnter(player);
        if (success) {
            foreach (Room r in rooms) {
                r.SetColor(Color.white);
            }

            room.SetColor(Color.green);
            foreach (Node n in room.node.neighbors) {
                n.room.SetColor(Color.yellow);
            }

            currentRoom = room;
        }
        
        return success;
    }
}
