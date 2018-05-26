using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {

	public float speed;

	public List<GameObject> followers;

	public Vector2[] prevPos;

	public int frameCount;
	public int frameIndex = 0;

	public bool IsMoving = true;


	// Use this for initialization
	void Start () {
		followers = new List<GameObject> ();
		prevPos = new Vector2[10];
	}
	
	// Update is called once per frame
	void Update () {
		if (frameIndex == 0){
			UpdatePrev ();
		}

		Movement ();

		FollowerMovement ();

		IndexControl ();

		CheckMoving ();
	}

	void CheckMoving(){
		if ((Vector2)transform.position == prevPos[0]){
			IsMoving = false;
		}
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
		Vector2 tempPos = transform.position;
		if (Input.GetKey(KeyCode.A)){
			tempPos.x -= speed;
		}
		if (Input.GetKey(KeyCode.D)){
			tempPos.x += speed;

		}
		if (Input.GetKey(KeyCode.W)){
			tempPos.y += speed;

		}
		if (Input.GetKey(KeyCode.S)){
			tempPos.y -= speed;

		}
		transform.position = tempPos;
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
			}
		}
	}

}
