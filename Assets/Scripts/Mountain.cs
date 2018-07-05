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
    private BoardManager boardManager = new BoardManager();

    public void Generate( List<Node> graph, Node parent) {
        foreach( Node n in graph) {
            GameObject roomObj = Instantiate(roomPrefab, canvas.transform);
            Vector3 position = new Vector3(Random.Range(-250f, 250f), Random.Range(-200f, 200f), 0f);
            if (parent != null)
            {
                position = parent.room.gameObject.transform.localPosition;
            }
            roomObj.transform.localPosition = position;
            Room room = roomObj.GetComponent<Room>();
            room.node = n;
            n.room = room;
            rooms.Add(room);
        }
        if (parent == null)
        {
            rooms[0].SetAttractionLocation(Vector3.zero);
            rooms[rooms.Count - 1].SetAttractionLocation(new Vector3(250f, 250f, 0f));
        }

        Debug.Log("starting board manager");
        boardManager.Initialize(rooms);

        for (int i = 0; i < 1000; i++)
        {
            boardManager.Step();
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
            if (Random.Range(0, 1) > .6f)
            {
                A.neighbors.Add(C);
                C.neighbors.Add(A);
            }

            List<Node> graph = new List<Node>();
            graph.Add(A);
            graph.Add(B);
            graph.Add(C);

            Node parent = null;
            if (rooms.Count > 0)
            {
                parent = rooms[Random.Range(0, rooms.Count)].node;
                parent.neighbors.Add(A);
                A.neighbors.Add(parent);
            }

            Generate(graph, parent);
        }
        if (start) {
            start = false;

            player = new Player();

            foreach(Room r in rooms) {
                r.InitializeView();
            }
        }
        boardManager.Step();
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
