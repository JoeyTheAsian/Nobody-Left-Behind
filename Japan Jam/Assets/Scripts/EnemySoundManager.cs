using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemySoundManager : MonoBehaviour {
    public AudioSource breathSource;
    public AudioSource footstepSource;

    public List<AudioClip> breathSounds;
    public List<AudioClip> footstepSounds;

    float footstepTimer;
    float breathTimer;
    //more than 2
    public float breathTime;
    public float footstepTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(breathTimer <= 0f)
        {
            breathTimer = Random.Range(2f, breathTime);
            breathSource.clip = breathSounds[Random.Range(0, breathSounds.Count)];
            breathSource.Play();
        }
        breathTimer -= Time.deltaTime;
        if (footstepTimer <= 0f)
        {
            footstepTimer = (footstepTime + Random.Range(0f,.15f));
            footstepSource.clip = footstepSounds[Random.Range(0, footstepSounds.Count)];
            footstepSource.Play();
        }
        footstepTimer -= Time.deltaTime;
    }
}
