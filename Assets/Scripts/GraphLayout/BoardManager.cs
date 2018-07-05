using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BoardManager
{
    private List<Body> bodys = new List<Body>();
    private bool displayQuad = true;
    private Boundary boundary;
    private QuadNode quadTree;
    private List<Quad> quads = new List<Quad>();

    public void Initialize(List<Room> nodes)
    {
        boundary = new Boundary(100);
        foreach (Room node in nodes) {
            bodys.Add(new Body(node.gameObject));
        }
    }

    public void Step()
    {
        if (bodys.Count > 0)
        {
            boundary.update(bodys);
            float sized = Mathf.Max((boundary.max.x - boundary.min.x), (boundary.max.y - boundary.min.y));
            Vector3 center = new Vector3((boundary.max.x + boundary.min.x) / 2, (boundary.max.y + boundary.min.y) / 2, (boundary.max.z + boundary.min.z) / 2);
            quadTree = new QuadNode(1, center, sized);
            foreach (Body bod in bodys)
            {
                quadTree.addBody(bod);
            }
            BarnesHut();
        }
    }

    void BarnesHut()
    {
        foreach (Body body in bodys)
        {
            quadTree.interact(body, 0.5f);
            body.update();
        }
    }
}