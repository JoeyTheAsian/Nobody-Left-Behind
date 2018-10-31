using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitScript : MonoBehaviour {

	public bool IsOpen = false;
	public int followCount;

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<playerControl>().followers.Count  >= followCount && !IsOpen){
			IsOpen = true;
			GetComponents<BoxCollider2D> ()[1].enabled = false;
		}
	}
}
