using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterScript : MonoBehaviour {

	public bool collected = false;

	public float maxSpeed;
	public float maxForce;
	public Vector2 acceleration;
	public Vector2 velocity;
	public Vector2 position;

	public GameObject player;

	public Vector2 seekPos;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}

	// Update is called once per frame
	void Update () {
		if(player.GetComponent<playerControl>().IsMoving && collected){
			acceleration = Vector2.zero;
			Vector2 ultimate = Vector2.zero;
			Vector2 tempPos = transform.position;
			Seek ();
			velocity += acceleration;
			velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
			tempPos += velocity;
			transform.position = tempPos;
		}
		if(!player.GetComponent<playerControl>().IsMoving && collected){
			SlowDown ();
		}

	}

	void Seek(){
		Vector2 result = Vector2.zero;
		result = seekPos - (Vector2)transform.position;
		result.Normalize ();
		result *= maxForce;
		ApplyForce (result);
	}

	void ApplyForce(Vector2 force){
		acceleration += force;
	}

	void SlowDown(){
		Vector2 tempPos = transform.position;
		velocity *= 0.8f;
		if(velocity.magnitude < 0.1f){
			velocity = Vector2.zero;
		}
		tempPos += velocity;
		transform.position = tempPos;
	}
}
