using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {
    float timer;
    public AudioSource efxSource;

    // Use this for initialization
    void Start () {
        //efxSource = GetComponent<AudioSource>();
        timer = 5f;
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0 && Random.Range(0, 10) == 0 && efxSource.isPlaying == false) {
            Debug.Log("Playing");
            efxSource.Play();
           
            timer = 5f; 
        }
    }
}
