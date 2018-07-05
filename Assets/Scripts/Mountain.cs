using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : MonoBehaviour {

    //public struct Node {
    //    public List<Node> neighbors;

    //    public Node() {
    //        neighbors = new List<Node>();
    //    }
    //}

    public bool button = false;
    public GameObject roomPrefab;
    public Canvas canvas;

    public void Generate( List<Node> graph) {
        foreach( Node n in graph) {
            GameObject room = Instantiate(roomPrefab, canvas.transform);
            room.transform.localPosition = new Vector3(Random.Range(-150f, 150f), Random.Range(-120f, 120f), 0f);
            //GameObject room = Instantiate(roomPrefab, new Vector3(100f, 100f, 0f), Quaternion.identity);
            //GameObject room = Instantiate(roomPrefab, new Vector3(Random.Range(-150f, 150f), Random.Range(-120f, 120f)), Quaternion.identity);
            //room.transform.SetParent(canvas.transform, true);
        }
    }

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
    }
}
