using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public float boundsHeight;
	public float boundWidth;
	public GameObject tree;
	public GameObject character;
	public GameObject exit;

	public float treeNum;
	public float characterNum;

	private Vector2 topLeft;
	private float spawnDistance;

	// Use this for initialization
	void Start () {
		topLeft = new Vector2 (-boundWidth / 2, boundsHeight / 2);
		spawnDistance = 5f;
		SpawnBounds ();
		SpawnObstacles ();
		SpawnCharacters ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
	void SpawnBounds(){
		for (int i = 0; i < boundsHeight * 2; i+=(int)spawnDistance) {
			Instantiate (tree, new Vector2 ((-boundWidth / 2), boundsHeight - i), Quaternion.identity);
		}
		for (int i = 0; i < boundsHeight * 2; i+=(int)spawnDistance) {
			Instantiate (tree, new Vector2 ((boundWidth / 2), boundsHeight - i), Quaternion.identity);
		}
		for (int i = 0; i < boundWidth; i+=(int)spawnDistance) {
			Instantiate (tree, new Vector2 (boundWidth / 2 - i, boundsHeight), Quaternion.identity);
		}
		for (int i = 0; i < boundWidth / 2; i+=(int)spawnDistance) {
			//Instantiate (exit, new Vector2 (boundWidth - i, -boundsHeight), Quaternion.identity);
			Instantiate (tree, new Vector2 (boundWidth / 2 - i, -boundsHeight), Quaternion.identity);
		}
		Instantiate (exit, new Vector2 (0, -boundsHeight), Quaternion.identity);
		for (int i = (int)boundWidth / 2 + (int)spawnDistance; i < boundWidth; i+=(int)spawnDistance) {
			//Instantiate (exit, new Vector2 (boundWidth - i, -boundsHeight), Quaternion.identity);
			Instantiate (tree, new Vector2 (boundWidth / 2 - i, -boundsHeight), Quaternion.identity);
		}
	}

	void SpawnObstacles(){
		for (int i = 0; i < treeNum; i++) {
			float rX = Random.Range (topLeft.x, topLeft.x + boundWidth);
			float rY = Random.Range (topLeft.y, topLeft.y + boundsHeight * 2) - boundsHeight * 1.5f;
			Instantiate (tree, new Vector2 (rX, rY), Quaternion.identity);
		}
	}

	void SpawnCharacters(){
		for (int i = 0; i < characterNum; i++) {
			float rX = Random.Range (topLeft.x, topLeft.x + boundWidth);
			float rY = Random.Range (topLeft.y, topLeft.y + boundsHeight * 2) - boundsHeight * 1.5f;
			Instantiate (character, new Vector2 (rX, rY), Quaternion.identity);
		}
	}
}
