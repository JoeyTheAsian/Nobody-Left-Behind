using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {

	public float speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
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

}
