using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {
    List<Node> nodes = new List<Node>();

    void addNode(Node a){
        nodes.Add(a);
    }

    public void connectNodes(Node a, Node b) {
        a.neighbors.Add(b);
        b.neighbors.Add(a);
    }

    public Graph findSubGraph(Graph a){
        return a;
    }



}
