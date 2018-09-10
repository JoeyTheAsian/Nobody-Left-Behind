using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayer : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public bool tree;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (tree)
        {
            spriteRenderer.sortingOrder = -1 * (int)(this.transform.parent.transform.position.y * 100);
        }
        else
        {
            spriteRenderer.sortingOrder = -1 * (int)(this.transform.position.y * 100);
        }
    }
}
