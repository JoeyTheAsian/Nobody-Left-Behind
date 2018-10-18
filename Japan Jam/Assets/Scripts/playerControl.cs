using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playerControl : MonoBehaviour {

	public Vector2 speedForce;
	private Vector2 speedForceVert;

	public float maxSpeed;
	public float maxForce;
	public Vector2 acceleration;
	public Vector2 velocity;
	public Vector2 position;
    public AudioSource footstep;

	public List<GameObject> followers;

	public Vector2[] prevPos;

    public Sprite up;
    public Sprite down;
    public Sprite right;
    public Sprite left;
    public RectTransform credits;

    public GameObject bloodParticles;

    public SpriteRenderer spriteRenderer;

    public int frameCount;
	public int frameIndex = 0;

	public bool IsMoving = false;

    public bool CanControl = true;
    float exitTimer = 10f;

    bool hit = false;
    public static float hitFlashTime = .5f;
    float hitFlashTimer;

    public Image healthImage;
    public int health;
    public Manager manager;

    public AudioSource gruntSource;
    public AudioSource flashlightSource;

    public GameObject flashLight;
    public Image flashlightCooldownBar;
    public float flashlightCooldownTime;
    public float flashlightCooldownTimer;
    public float flashlightActiveTime;
    float flashlightActiveTimer;
    public bool flashlightActive = false;
    public bool flashlightCooldown = false;

    public float levelLoadTime;
    public float levelLoadTimer;
    public Image fadePanel;
	// Use this for initialization
	void Start () {
		followers = new List<GameObject> ();
		prevPos = new Vector2[10];
		speedForceVert = Vector2.up * speedForce.magnitude;
        hitFlashTimer = hitFlashTime;
        flashlightCooldownTimer = flashlightCooldownTime;
        flashlightActiveTimer = flashlightActiveTime;
        levelLoadTimer = levelLoadTime;
	}
	
	// Update is called once per frame
	void Update () {
        healthImage.transform.localScale = new Vector2(health / 3f, healthImage.transform.localScale.y);
        if (health <= 0) {
            CanControl = false;
        }
        if (flashlightActive) {
            flashlightActiveTimer -= Time.deltaTime;
            
            if(flashlightActiveTimer <= 0f)
            {
                flashlightActive = false;
                flashLight.SetActive(false);
                flashlightSource.Play();
                flashlightActiveTimer = flashlightActiveTime;
            }
            else
            {
                flashLight.SetActive(true);
                flashlightSource.Play();
            }
        }
        if (flashlightCooldownTimer <= 0)
        {
            flashlightCooldown = false;
            flashlightCooldownTimer = flashlightCooldownTime;
        }
        if (flashlightCooldown) {
            flashlightCooldownTimer -= Time.deltaTime;
            flashlightCooldownBar.rectTransform.localScale = new Vector2(flashlightCooldownTimer/flashlightCooldownTime, flashlightCooldownBar.transform.localScale.y);
        }
		position = transform.position;
		Vector2 ultimate = Vector2.zero;
		if (frameIndex == 0){
			UpdatePrev ();
		}
        if (hit)
        {
            spriteRenderer.color = Color.red;
            hitFlashTimer -= Time.deltaTime;
            if(hitFlashTimer <= 0f)
            {
                hit = false;
                hitFlashTimer = hitFlashTime;
                spriteRenderer.color = Color.white;
            }
        }
		if(CanControl){
			Movement ();
		}
		else if (health > 0){
            AutoMove();
            if(levelLoadTimer > 0f)
            {
                levelLoadTimer -= Time.deltaTime;
                fadePanel.color = new Vector4(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, (1f-(levelLoadTimer/levelLoadTime)));
            }
            else
            {
                manager.LevelUp();
                manager.GenerateLevel();
                health = 3;
                followers.Clear();
                gameObject.transform.position = new Vector3(0f, 0f, 0f);
                fadePanel.color = new Vector4(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0f);
                CanControl = true;
                levelLoadTimer = levelLoadTime;
            }
        }
        //game over
        else
        {
            manager.GameOver();
        }

		FollowerMovement ();

		IndexControl ();

		CheckMoving ();
        if (IsMoving && !footstep.isPlaying)
        {
            footstep.Play();
        }
        if (!IsMoving)
        {
            footstep.Stop();
        }
        if (!CanControl)
        {
            //credits.transform.Translate(new Vector3(0f, Time.deltaTime * 10.0f, 0f));
            exitTimer -= Time.deltaTime;
            if(exitTimer <= 0f)
            {

                //SceneManager.LoadScene("Main Menu");
            }
        }
    }

	void ApplyForce(Vector2 force){
		acceleration += force;
	}

	void CheckMoving(){
		if((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D)) && CanControl){
			IsMoving = false;
			velocity = Vector2.zero;
			acceleration = Vector2.zero;
		}
		/*if ((Vector2)transform.position == prevPos[0]){
			IsMoving = false;
			velocity = Vector2.zero;
			acceleration = Vector2.zero;
		}*/
		else{
			IsMoving = true;
		}
	}

	void IndexControl(){
		frameIndex++;
		if (frameIndex == frameCount){
			frameIndex = 0;
		}
	}
    void SetFlashlight(string dir)
    {
        if (dir == "Right")
        {
            flashLight.transform.rotation = Quaternion.Euler(new Vector3(0f,0f, 90f));
        }
        else if (dir == "Left")
        {
            flashLight.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
        }
        else if (dir == "Up")
        {
            flashLight.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
        else if (dir == "Down")
        {
            flashLight.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }
    void SetSprite(string dir)
    {
        if (dir == "Right")
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("D");
            //GetComponent<SpriteRenderer>().sprite = right;
        }
        else if (dir == "Left")
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("A");
            // GetComponent<SpriteRenderer>().sprite = left;
        }
        else if (dir == "Up")
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("W");
            // GetComponent<SpriteRenderer>().sprite = up;
        }
        else if (dir == "Down")
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("S");
            // GetComponent<SpriteRenderer>().sprite = down;
        }
        else if(dir == "Stop")
        {
            GetComponent<Animator>().speed = 0;
        }
    }
	void Movement(){
        if (Input.GetKey(KeyCode.Space) && !flashlightCooldown) {
            flashlightActive = true;
            flashlightCooldown = true;
        }
		//Vector2 tempPos = transform.position;
		if (Input.GetKey(KeyCode.A)){
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                SetSprite("Left");
            }
            SetFlashlight("Left");
			//position.x -= speed;
			ApplyForce(-speedForce);
		}
		if (Input.GetKey(KeyCode.D)){
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                SetSprite("Right");
            }
            SetFlashlight("Right");
            //position.x += speed;
            ApplyForce(speedForce);

		}
		if (Input.GetKey(KeyCode.W)){
            SetSprite("Up");
            SetFlashlight("Up");
            //position.y += speed;
            ApplyForce(speedForceVert);
		}
		if (Input.GetKey(KeyCode.S)){
            SetSprite("Down");
            SetFlashlight("Down");
            //position.y -= speed;
            ApplyForce(-speedForceVert);

		}
        if(!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            SetSprite("Stop");
        }
		velocity += acceleration;
		velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		acceleration = Vector2.zero;
		position += velocity;
		transform.position = position;
		//transform.position = tempPos;
	}

	void AddFollower(GameObject follower){
		followers.Add (follower);
	}

	void FollowerMovement(){
		for(int i = 0; i < followers.Count; i++){
			followers [i].GetComponent<characterScript>().seekPos = prevPos[i];
		}
	}

	void UpdatePrev(){
		Vector2[] tempArr = new Vector2[9];

		for (int i = 0; i < 9; i++) {
			//Debug.Log (i + 1);
			tempArr [i] = prevPos [i];
		}
		//Debug.Log (tempArr);

		prevPos [0] = transform.position;

		for (int i = 0; i < 9; i++) {
			prevPos[i+1] = tempArr[i];
		}

	}

	void AutoMove(){
		velocity = -speedForceVert * maxSpeed;
		velocity = Vector2.ClampMagnitude (velocity, maxSpeed);
		position = transform.position;
		position += velocity;
		transform.position = position;

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.name.Contains("character")){
			if (!col.GetComponent<characterScript>().collected){
				col.GetComponent<characterScript> ().collected = true;
				AddFollower (col.gameObject);
				Debug.Log ("collected");
			}
		}
        else if (col.name.Contains("enemy"))
        {
            if (!hit)
            {

                Debug.Log("enemy hit");
                if (health > 0)
                {
                    health--;
                    bloodParticles.SetActive(true);
                    gruntSource.Play();
                }
                hit = true;
                transform.position += (transform.position - col.gameObject.transform.position).normalized * maxForce * 6f;
            }
        }else if (col.name.Contains("trap"))
        {
            if (!hit)
            {
                if (health > 0)
                {
                    health--;
                    bloodParticles.SetActive(true);
                    gruntSource.Play();
                }
                hit = true;
                transform.position += (transform.position - col.gameObject.transform.position).normalized * maxForce * 6f;
            }
        }
        else if(col.name.Contains("exit")){
			if (col.GetComponent<exitScript>().IsOpen){
				Debug.Log ("exit!");
				CanControl = false;

			}
		}
	}

}
