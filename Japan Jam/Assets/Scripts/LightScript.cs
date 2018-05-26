using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

	public float intensity;
	public float range;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ManageLight ();
	}

	void ManageLight(){
		int followCount = gameObject.GetComponent<playerControl> ().followers.Count;

		intensity = 1f + followCount;
		range = 17f + 3f * followCount;

		gameObject.GetComponentInChildren<Light> ().intensity = intensity;
		gameObject.GetComponentInChildren<Light> ().range = range;

	}
}
