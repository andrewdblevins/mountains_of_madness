using UnityEngine;
using System.Collections;
using System;

public class Body  {

	private GameObject dot;
	public float mass;
	public Vector3 position;
	private Vector3 velocity;
	private Vector3 acceleration;
	private float attractConstant = .00003f;
    private float repulseConstant = 100f;
    private float originAttractionConstant = .0001f;
    private float drag = .90f;
	public Guid InstanceID {get; private set;}

	public Body(GameObject _dot){
		this.InstanceID = Guid.NewGuid();
		mass = 2.0f;
		dot = _dot;
		velocity = Vector3.zero;
		acceleration = Vector3.zero;
		if(_dot != null)
			position = CopyVector(_dot.transform.position);
		 else 
			position = Vector3.zero;
	}

	public void update(){
        Room room = dot.GetComponent<Room>();
        foreach(Node node in room.node.neighbors)
        {
            this.applyForce(attractNeighbor(node.room.gameObject));
        }

        applyForce(attractOrigin());

        velocity += acceleration;
		position += velocity;
        velocity *= drag;
		acceleration = Vector3.zero;

		dot.transform.position = CopyVector(position);
    }

	public void interac(Body b){
		this.applyForce(b.repulse(this));
	}

	public void applyForce(Vector3 force){
        acceleration += new Vector3(force.x/mass,force.y/mass,force.z/mass);
	}

    public Vector3 attractNeighbor(GameObject b)
    {
        Vector3 forc = position - b.transform.position;
        float distance = forc.magnitude;
        distance = Mathf.Clamp(distance, 5f, 250f);

        forc.Normalize();
        float strenght = -(attractConstant * mass * mass) * (distance * distance);
        return new Vector3(forc.x * strenght, forc.y * strenght, forc.z * strenght);
    }

    public Vector3 attractOrigin()
    {
        Vector3 forc = position - Vector3.zero;
        float distance = forc.magnitude;
        distance = Mathf.Clamp(distance, 5f, 250f);

        forc.Normalize();
        float strenght = -(originAttractionConstant * mass * mass) * (distance * distance);
        return new Vector3(forc.x * strenght, forc.y * strenght, forc.z * strenght);
    }


    public Vector3 repulse(Body b){
		Vector3 forc = position - b.position;
		float distance = forc.magnitude;
		distance = Mathf.Clamp(distance,5f,250f);

		forc.Normalize();
		float strenght = -(repulseConstant * mass*mass)/(distance*distance);
		return new Vector3(forc.x*strenght,forc.y*strenght,forc.z*strenght);
	}

	public void addBody(Body body){
		float m = mass + body.mass;
		float x = (position.x * mass + body.position.x * body.mass) / m ;
		float y = (position.y * mass + body.position.y * body.mass) / m ;
		mass = m;
		position = new Vector3(x,y,0);
	}
	
	private Vector3 CopyVector(Vector3 vec) {
		return new Vector3(vec.x,vec.y,vec.z);
	}
}