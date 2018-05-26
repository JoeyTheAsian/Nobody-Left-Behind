using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public float boundsHeight;
	public float boundWidth;
	public GameObject tree;

	private Vector2 topLeft;
	private float spawnDistance;

	// Use this for initialization
	void Start () {
		topLeft = new Vector2 (-boundWidth / 2, boundsHeight / 2);
		spawnDistance = 7f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
	void SpawnBounds(){
		for (int i = 0; i < boundsHeight / spawnDistance; i+=spawnDistance) {
			Instantiate (tree, new Vector2 (-boundWidth / 2), 9);
		}
	}
}
