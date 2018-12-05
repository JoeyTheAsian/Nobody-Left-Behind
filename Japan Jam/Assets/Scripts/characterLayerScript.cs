using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterLayerScript : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        int playerLayer = player.GetComponent<SpriteRenderer>().sortingOrder;
        if(Mathf.Abs(player.GetComponent<SpriteRenderer>().sortingOrder - (-1 * (int)(this.transform.position.y * 100))) < 2){
            if (player.GetComponent<playerControl>().followers.IndexOf(gameObject) > 0)
            {
                int followerNumber = player.GetComponent<playerControl>().followers.IndexOf(gameObject);
                spriteRenderer.sortingOrder = player.GetComponent<playerControl>().followers[followerNumber - 1].GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
            else
            {
                spriteRenderer.sortingOrder = playerLayer - 1;
            }
        }
        else
        {
            spriteRenderer.sortingOrder = -1 * (int)(this.transform.position.y * 100);
        }
        

    }
}
