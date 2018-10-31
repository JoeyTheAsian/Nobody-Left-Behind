using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour {
    float changeTimer ;
    public float changeTime = .29f;
    public Image title;
    int spriteIndex = 0;
    public Sprite[] titleSprites = new Sprite[2];
	// Use this for initialization
	void Start () {
        changeTimer = changeTime;
	}
	
	// Update is called once per frame
	void Update () {
        changeTimer -= Time.deltaTime;
        if(changeTimer <= 0f)
        {
            changeTimer = changeTime;
            if(spriteIndex == 0)
            {
                spriteIndex = 1;
            }
            else {
                spriteIndex = 0;
            }
            title.sprite = titleSprites[spriteIndex];
        }
	}
}
