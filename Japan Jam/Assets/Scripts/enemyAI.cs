using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

	public float maxSpeed;
	public float maxForce;
	public Vector2 acceleration;
	public Vector2 velocity;
	public Vector2 position;

	public GameObject player;

	// Use this for initialization
	void Start () {
		position = transform.position;
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		acceleration = Vector2.zero;
		Vector2 ultimate = Vector2.zero;
		Vector2 tempPos = transform.position;
		Seek (player);
		velocity += acceleration;
		velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		tempPos += velocity;
		transform.position = tempPos;
	}

	void Seek(GameObject p){
		Vector2 result = Vector2.zero;
		result = p.transform.position - transform.position;
		result.Normalize ();
		result *= maxForce;
		ApplyForce (result);
	}

	void ApplyForce(Vector2 force){
		acceleration += force;
	}
}
