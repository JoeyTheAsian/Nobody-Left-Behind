﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

	public float maxSpeed;
	public float maxForce;
	public Vector2 acceleration;
	public Vector2 velocity;
	public Vector2 position;
	public float aggroRadius;
	public float wanderDistance;
	public Vector2 wanderPoint;
	public bool IsWandering = false;
	public float wanderRadius;

	public bool CanFlee = false;

	public List<GameObject> followers;

	public GameObject player;
    GameObject target;
	// Use this for initialization
	void Start () {
		position = transform.position;
		player = GameObject.Find ("player");
        target = player;
	}
	
	// Update is called once per frame
	void Update () {
		acceleration = Vector2.zero;
		Vector2 ultimate = Vector2.zero;
		Vector2 tempPos = transform.position;
		if(CheckDistance()){
			Seek (target.transform.position);
            Debug.Log(target.name);
			//RotateTowardsPlayer ();
		}
		else if(CanFlee){
			Flee (player.transform.position);
		}
		else{
			Wander ();
		}
		velocity += acceleration;
		velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		tempPos += velocity;
		transform.position = tempPos;

		ManageFollowers ();
	}

	void Seek(Vector2 p){
		Vector2 result = Vector2.zero;
		result = p - (Vector2)transform.position;
		result.Normalize ();
		result *= maxForce;
		ApplyForce (result);
	}

	void Flee(Vector2 p){
		Vector2 result = Vector2.zero;
		result = (Vector2)transform.position - p;
		result.Normalize ();
		result *= maxForce;
		ApplyForce (result);
	}

	void ApplyForce(Vector2 force){
		acceleration += force;
	}

	bool CheckDistance(){
		if((player.transform.position - transform.position).magnitude < aggroRadius && !CanFlee){
            target = player;
			return true;
		}
        //check agains followers as well
        foreach(GameObject follower in player.GetComponent<playerControl>().followers)
        {
            if((follower.transform.position - transform.position).magnitude < aggroRadius 
                //check distance between target and replace if new target is closer
                && (follower.transform.position - transform.position).magnitude < (target.transform.position - transform.position).magnitude)
            {
                target = follower;
                return true;
            }
        }
		return false;
	}

	void SetWanderPoint(){
		float angle;
		angle = Random.Range (0, 360);
		//angle = angle * Mathf.Deg2Rad;
		Vector2 seekPoint;
		transform.Rotate (new Vector3 (0f, 0f, angle));

		seekPoint = transform.up * wanderDistance;
		transform.rotation = Quaternion.identity;
		wanderPoint = seekPoint;
	}

	void Wander(){
		if((wanderPoint - (Vector2)transform.position).magnitude < wanderRadius || !IsWandering){
			SetWanderPoint ();
			IsWandering = true;
		}
		Seek (wanderPoint);
	}

	void RotateTowardsPlayer(){
		Vector2 rot = transform.position - player.transform.position;
		rot.Normalize ();
		transform.right = rot;
	}

	void ManageFollowers(){
		foreach(GameObject f in followers){
			f.GetComponent<characterScript> ().seekPos = transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		SetWanderPoint ();
		Debug.Log ("col");
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("stolen");
		if(col.name.Contains("character")){
			/*for(int i = 0; i < player.GetComponent<playerControl> ().followers.Count; i++){
				if (col == player.GetComponent<playerControl> ().followers[i]){
					player.GetComponent<playerControl> ().followers.RemoveAt (i);
					break;
				}
			}*/
			player.GetComponent<playerControl> ().followers.Remove (col.gameObject);
			CanFlee = true;
			col.gameObject.GetComponent<characterScript> ().collected = false;
			col.gameObject.GetComponent<characterScript> ().IsStolen = true;
			col.gameObject.GetComponent<characterScript> ().seekPos = gameObject.transform.position;
			followers.Add (col.gameObject);

		}
	}
}
