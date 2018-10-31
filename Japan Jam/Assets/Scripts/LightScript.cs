using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

	public float intensity;
	public float range;

    float flicker;
    // Use this for initialization
    void Start () {
        flicker = Random.Range(-.5f, .5f);
    }
	
	// Update is called once per frame
	void Update () {
		ManageLight (Time.deltaTime);
	}

    void ManageLight(float deltaTime) {
        int followCount = gameObject.GetComponent<playerControl>().followers.Count;

        intensity = 1f + followCount/2f;
        range = 17f + 1.5f * followCount;

        if (Mathf.Abs(gameObject.GetComponentInChildren<Light>().intensity - (intensity + flicker)) < .05f)
        {
            flicker = Random.Range(-.5f, .5f);
        }
        
		gameObject.GetComponentInChildren<Light> ().intensity = Mathf.Lerp(gameObject.GetComponentInChildren<Light>().intensity, intensity + flicker, 3 * Time.deltaTime);
		gameObject.GetComponentInChildren<Light> ().range = Mathf.Lerp(gameObject.GetComponentInChildren<Light>().range, range + flicker, 3* Time.deltaTime);

    }
}
