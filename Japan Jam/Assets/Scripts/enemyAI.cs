using System.Collections;
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

	public GameObject player;
    GameObject target;
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
		if(CheckDistance()){
			Seek (target.transform.position);
            Debug.Log(target.name);
			//RotateTowardsPlayer ();
		}
		else{
			Wander ();
		}
		velocity += acceleration;
		velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		tempPos += velocity;
		transform.position = tempPos;
	}

	void Seek(Vector2 p){
		Vector2 result = Vector2.zero;
		result = p - (Vector2)transform.position;
		result.Normalize ();
		result *= maxForce;
		ApplyForce (result);
	}

	void ApplyForce(Vector2 force){
		acceleration += force;
	}

	bool CheckDistance(){
		if((player.transform.position - transform.position).magnitude < aggroRadius){
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

	void OnCollisionEnter2D(Collision2D col){
		SetWanderPoint ();
		Debug.Log ("col");
	}
}
