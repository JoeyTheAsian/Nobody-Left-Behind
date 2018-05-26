using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {

    public AudioSource efxSource;

    // Use this for initialization
    void Start () {
        efxSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Random.Range(0, 100) == 0 && efxSource.isPlaying == false) {
            efxSource.Play();
        }
    }
}
