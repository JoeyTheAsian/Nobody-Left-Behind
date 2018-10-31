using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

	public float maxSpeed;
	public float maxForce;
	public Vector2 acceleration;
	public Vector2 velocity;
	public Vector2 position;
	public float aggroRadius;
	public float wanderDistance;
	public Vector2 wanderPoint;
	public bool IsWandering = false;
	public float wanderRadius;
    public Sprite up;
    public Sprite down;
    public Sprite right;
    public Sprite left;

    public bool CanFlee = false;

	public List<GameObject> followers;

    public GameObject follow;

	public GameObject player;
    GameObject target;
	// Use this for initialization
	void Start () {
		position = transform.position;
		player = GameObject.Find ("player");
        target = player;
	}
	
	// Update is called once per frame
	void Update () {
		acceleration = Vector2.zero;
		Vector2 ultimate = Vector2.zero;
		Vector2 tempPos = transform.position;
		if(CheckDistance()){
			Seek (target.transform.position);
			//RotateTowardsPlayer ();
		}
		else if(CanFlee){
			Flee (player.transform.position);
		}
		else{
			Wander ();
		}
		velocity += acceleration;
		velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		tempPos += velocity * 33f * Time.deltaTime;
        Vector3 direction = velocity.normalized;

		transform.position = tempPos;
        float angle = Mathf.Atan2(direction.x, direction.y) * (180f / Mathf.PI);
        if(angle < 0f)
        {
            angle += 360f;
        }
        if(angle >= 45f && angle < 135f)
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
        transform.position = tempPos;

		ManageFollowers ();
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
    void Seek(Vector2 p){
		Vector2 result = Vector2.zero;
		result = p - (Vector2)transform.position;
		result.Normalize ();
		result *= maxForce;
		ApplyForce (result);
	}

	void Flee(Vector2 p){
		Vector2 result = Vector2.zero;
		result = (Vector2)transform.position - p;
		result.Normalize ();
		result *= maxForce;
		ApplyForce (result);
	}

	void ApplyForce(Vector2 force){
		acceleration += force;
	}

	bool CheckDistance(){
		if((player.transform.position - transform.position).magnitude < aggroRadius && !CanFlee){
            target = player;
			return true;
		}
        else if (CanFlee)
        {
            if((player.transform.position - transform.position).magnitude > aggroRadius * 2 && CanFlee)
            {
                CanFlee = false;
                Manager man = GameObject.Find("Manager").GetComponent<Manager>();

                foreach (GameObject f in followers)
                {
                    float rX = Random.Range(man.topLeft.x + man.boundWidth / 10, man.topLeft.x + man.boundWidth * 0.9f);
                    float rY = Random.Range(man.topLeft.y + man.boundsHeight / 10, man.topLeft.y + man.boundsHeight * 2 * 0.9f) - man.boundsHeight * 1.5f;
                    Instantiate(follow, new Vector2(rX, rY), Quaternion.identity);
                    //followers.Remove(f);
                    Destroy(f);
                }
                followers.Clear();
            }
            return false;
        }
        //check agains followers as well
        foreach(GameObject follower in player.GetComponent<playerControl>().followers)
        {
            if((follower.transform.position - transform.position).magnitude < aggroRadius 
                //check distance between target and replace if new target is closer
				&& (follower.transform.position - transform.position).magnitude < (target.transform.position - transform.position).magnitude && follower.GetComponent<characterScript>().collected)
            {
                target = follower;
                return true;
            }
        }
		return false;
	}

	void SetWanderPoint(){
        /*float angle;
		angle = Random.Range (0, 360) * Mathf.Deg2Rad;
		//angle = angle * Mathf.Deg2Rad;
		Vector2 seekPoint;
        //transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);

        Vector2 plusVec = Vector2.up;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);
        plusVec.x = (cos * plusVec.x) - (sin * plusVec.y);
        plusVec.y = (cos * plusVec.y) + (sin * plusVec.x);



        seekPoint = (Vector2)(transform.up * wanderDistance) + plusVec;
		//transform.rotation = Quaternion.identity;
		wanderPoint = (Vector2)transform.position + seekPoint;*/
        Manager man = GameObject.Find("Manager").GetComponent<Manager>();
        float rX = Random.Range(man.topLeft.x, man.topLeft.x + man.boundWidth);
        float rY = Random.Range(man.topLeft.y, man.topLeft.y + man.boundsHeight * 2) - man.boundsHeight * 1.5f;
        wanderPoint = new Vector2(rX, rY);
    }

	void Wander(){
        //Debug.Log(wanderPoint.x + "," + wanderPoint.y);
		if((wanderPoint - (Vector2)transform.position).magnitude < wanderRadius || !IsWandering){
			SetWanderPoint ();
			IsWandering = true;
		}
		Seek (wanderPoint);
	}

	void RotateTowardsPlayer(){
		Vector2 rot = transform.position - player.transform.position;
		rot.Normalize ();
		transform.right = rot;
	}

	void ManageFollowers(){
		foreach(GameObject f in followers){
			f.GetComponent<characterScript> ().seekPos = transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		SetWanderPoint ();
		//Debug.Log ("col");
	}

	void OnTriggerEnter2D(Collider2D col){
		//Debug.Log ("stolen");
        //Debug.Log(col.name);
		if(col.name.Contains("character")){
			/*for(int i = 0; i < player.GetComponent<playerControl> ().followers.Count; i++){
				if (col == player.GetComponent<playerControl> ().followers[i]){
					player.GetComponent<playerControl> ().followers.RemoveAt (i);
					break;
				}
			}*/
			if (col.gameObject.GetComponent<characterScript>().collected){
				player.GetComponent<playerControl> ().followers.Remove (col.gameObject);
				CanFlee = true;
				col.gameObject.GetComponent<characterScript> ().collected = false;
				col.gameObject.GetComponent<characterScript> ().IsStolen = true;
				col.gameObject.GetComponent<characterScript> ().seekPos = gameObject.transform.position;
				followers.Add (col.gameObject);
			}
		}
        else if (col.name.Contains("flashlight"))
        {
            CanFlee = true;
        }
	}
}
