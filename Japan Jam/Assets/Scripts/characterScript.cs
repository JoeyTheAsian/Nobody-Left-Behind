using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterScript : MonoBehaviour {

	public bool collected = false;

	public float maxSpeed;
	public float maxForce;
	public Vector2 acceleration;
	public Vector2 velocity;
	public Vector2 position;

	public GameObject player;

	public Vector2 seekPos;

	public bool IsStolen = false;
    public Sprite up;
    public Sprite down;
    public Sprite right;
    public Sprite left;

    // Use this for initialization
    void Start () {
		player = GameObject.Find ("player");
	}

	// Update is called once per frame
	void Update () {
		if(player.GetComponent<playerControl>().IsMoving && collected){
			acceleration = Vector2.zero;
			Vector2 ultimate = Vector2.zero;
			Vector2 tempPos = transform.position;
			Seek ();
			velocity += acceleration;
			velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
			tempPos += velocity;
			transform.position = tempPos;

            Vector3 direction = velocity.normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * (180f / Mathf.PI);
            if (angle < 0f)
            {
                angle += 360f;
            }
            if (angle >= 45f && angle < 135f)
            {
                SetSprite("Right");
            }
            if (angle >= 135f && angle < 225f)
            {
                SetSprite("Down");
            }
            if (angle >= 225f && angle < 315f)
            {
                SetSprite("Left");
            }
            if ((angle <= 360 && angle > 315) || (angle >= 0 && angle < 45f))
            {
                SetSprite("Up");
            }
        }
		if(!player.GetComponent<playerControl>().IsMoving && collected){
			SlowDown ();
		}

		if(IsStolen){
			acceleration = Vector2.zero;
			Vector2 ultimate = Vector2.zero;
			Vector2 tempPos = transform.position;
			Seek ();
			velocity += acceleration;
			velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
			tempPos += velocity;
			transform.position = tempPos;

            Vector3 direction = velocity.normalized;
            float angle = Mathf.Atan2(direction.x, direction.y) * (180f / Mathf.PI);
            if (angle < 0f)
            {
                angle += 360f;
            }
            if (angle >= 45f && angle < 135f)
            {
                SetSprite("Right");
            }
            if (angle >= 135f && angle <225f)
            {
                SetSprite("Down");
            }
            if (angle >= 225f && angle < 315f)
            {
                SetSprite("Left");
            }
            if ((angle <= 360 && angle > 315) || (angle >= 0 && angle < 45f))
            {
                SetSprite("Up");
            }
        }

	}
    void SetSprite(string dir)
    {
        if (dir == "Right")
        {
            GetComponent<SpriteRenderer>().sprite = right;
        }
        else if (dir == "Left")
        {
            GetComponent<SpriteRenderer>().sprite = left;
        }
        else if (dir == "Up")
        {
            GetComponent<SpriteRenderer>().sprite = up;
        }
        else if (dir == "Down")
        {
            GetComponent<SpriteRenderer>().sprite = down;
        }
    }
    void Seek(){
		Vector2 result = Vector2.zero;
		result = seekPos - (Vector2)transform.position;
		result.Normalize ();
		result *= maxForce;
		ApplyForce (result);
	}

	void ApplyForce(Vector2 force){
		acceleration += force;
	}

	void SlowDown(){
		Vector2 tempPos = transform.position;
		velocity *= 0.8f;
		if(velocity.magnitude < 0.1f){
			velocity = Vector2.zero;
		}
		tempPos += velocity;
		transform.position = tempPos;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "object")
        {
            Manager man = GameObject.Find("Manager").GetComponent<Manager>();
            float rX = Random.Range(man.topLeft.x, man.topLeft.x + man.boundWidth);
            float rY = Random.Range(man.topLeft.y, man.topLeft.y + man.boundsHeight * 2) - man.boundsHeight * 1.5f;
            transform.position = new Vector2(rX, rY);
        }
    }
}
