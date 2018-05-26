using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {

	public Vector2 speedForce;
	private Vector2 speedForceVert;

	public float maxSpeed;
	public float maxForce;
	public Vector2 acceleration;
	public Vector2 velocity;
	public Vector2 position;
    public AudioSource footstep;

	public List<GameObject> followers;

	public Vector2[] prevPos;

	public int frameCount;
	public int frameIndex = 0;

	public bool IsMoving = false;

	public bool CanControl = true;


	// Use this for initialization
	void Start () {
		followers = new List<GameObject> ();
		prevPos = new Vector2[10];
		speedForceVert = Vector2.up * speedForce.magnitude;
	}
	
	// Update is called once per frame
	void Update () {


		position = transform.position;
		Vector2 ultimate = Vector2.zero;
		if (frameIndex == 0){
			UpdatePrev ();
		}

		if(CanControl){
			Movement ();
		}
		else{
			AutoMove ();
		}

		FollowerMovement ();

		IndexControl ();

		CheckMoving ();
        if (IsMoving && !footstep.isPlaying)
        {
            footstep.Play();
        }
        if (!IsMoving)
        {
            footstep.Stop();
        }
    }

	void ApplyForce(Vector2 force){
		acceleration += force;
	}

	void CheckMoving(){
		if((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D)) && CanControl){
			IsMoving = false;
			velocity = Vector2.zero;
			acceleration = Vector2.zero;
		}
		/*if ((Vector2)transform.position == prevPos[0]){
			IsMoving = false;
			velocity = Vector2.zero;
			acceleration = Vector2.zero;
		}*/
		else{
			IsMoving = true;
		}
	}

	void IndexControl(){
		frameIndex++;
		if (frameIndex == frameCount){
			frameIndex = 0;
		}
	}

	void Movement(){
		//Vector2 tempPos = transform.position;
		if (Input.GetKey(KeyCode.A)){
			//position.x -= speed;
			ApplyForce(-speedForce);
		}
		if (Input.GetKey(KeyCode.D)){
			//position.x += speed;
			ApplyForce(speedForce);

		}
		if (Input.GetKey(KeyCode.W)){
			//position.y += speed;
			ApplyForce(speedForceVert);


		}
		if (Input.GetKey(KeyCode.S)){
			//position.y -= speed;
			ApplyForce(-speedForceVert);

		}
		velocity += acceleration;
		velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		acceleration = Vector2.zero;
		position += velocity;
		transform.position = position;
		//transform.position = tempPos;
	}

	void AddFollower(GameObject follower){
		followers.Add (follower);
	}

	void FollowerMovement(){
		for(int i = 0; i < followers.Count; i++){
			followers [i].GetComponent<characterScript>().seekPos = prevPos[i];
		}
	}

	void UpdatePrev(){
		Vector2[] tempArr = new Vector2[9];

		for (int i = 0; i < 9; i++) {
			//Debug.Log (i + 1);
			tempArr [i] = prevPos [i];
		}
		//Debug.Log (tempArr);

		prevPos [0] = transform.position;

		for (int i = 0; i < 9; i++) {
			prevPos[i+1] = tempArr[i];
		}

	}

	void AutoMove(){
		velocity = -speedForceVert * maxSpeed;
		velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		position = transform.position;
		position += velocity;
		transform.position = position;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.name.Contains("character")){
			if (!col.GetComponent<characterScript>().collected){
				col.GetComponent<characterScript> ().collected = true;
				AddFollower (col.gameObject);
				Debug.Log ("collected");
			}

		}
		else if(col.name.Contains("exit")){
			if (col.GetComponent<exitScript>().IsOpen){
				Debug.Log ("exit!");
				CanControl = false;

			}
		}
	}

}
