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

    public void Generate( List<Node> graph) {
        foreach( Node n in graph) {
            Instantiate(roomPrefab, new Vector3(Random.Range(-200, 200), Random.Range(-200, 200)), Quaternion.identity);
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
