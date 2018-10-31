using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public float boundsHeight;
	public float boundWidth;
	public List<GameObject> obstacles;
    public GameObject borderTree;
	public GameObject character;
	public GameObject exit;
    public GameObject enemy;
    public GameObject trap;

    public List<GameObject> items;

	public float treeNum;
	public int characterNum;
    public float enemyNum;
    public float trapNum;
    public float healthUpChance;
    public float speedUpChance;

	public Vector2 topLeft;
	float spawnDistance;
    public Transform maptransform;
    public GameObject gameOver;
    public DialogueManager dm;
    GameObject exitObj;

    bool dialogueStart = false;
	// Use this for initialization
	void Start () {
		topLeft = new Vector2 (-boundWidth / 2, boundsHeight / 2);
		spawnDistance = 5f;
		SpawnBounds ();
		SpawnObstacles ();
		SpawnCharacters ();
        SpawnEnemies();
        SpawnTraps();
        Time.timeScale = 0f;
        dm.AddDialogue("It's past sunset. You are lost deep in the woods and begin to hear strange noises.");
        dm.AddDialogue("You have to find your friends and get out of this forest.");
        dm.AddDialogue("All you have is this silly hand crank flashlight...         \nYou can just about keep it going on low power.         \nHigh drains it in less than a second.");
        dm.AddDialogue("Better find your friends quick and make it out of this forest.");
        dialogueStart = true;
     }

    // Update is called once per frame
    void Update () {
        if (dialogueStart)
        {
            dm.NextText();
            dialogueStart = false;
        }
	}
    public void GameOver()
    {
        gameOver.SetActive(true);
    }
    public void Reset()
    {
        SceneManager.LoadScene("test");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("main menu");
    }
    public void LevelUp()
    {
        boundsHeight *= 1.1f;
        boundWidth = boundsHeight * 2;
        enemyNum += 1;
        characterNum += 1;
        treeNum *= 1.2f;
        if(trapNum == 0f)
        {
            trapNum++;
        }
        trapNum *= 1.1f;
        
        topLeft = new Vector2(-boundWidth / 2, boundsHeight / 2);
    }
    public void GenerateLevel()
    {
        foreach (Transform t in maptransform)
        {
            GameObject.Destroy(t.gameObject);
        }
        SpawnBounds();
        SpawnObstacles();
        SpawnCharacters();
        SpawnEnemies();
        SpawnTraps();
    }
    void SpawnTraps(){
        for (int i = 0; i < trapNum; i++)
        {
            float rX = Random.Range(topLeft.x, topLeft.x + boundWidth);
            float rY = Random.Range(topLeft.y, topLeft.y + boundsHeight * 2) - boundsHeight * 1.5f;
            //int a = Random.Range(0, 3);
            Vector2 pos = new Vector2(rX, rY);
            if (Vector2.Distance(pos, exitObj.transform.position) > 14f)
            {
                Instantiate(trap, new Vector2(rX, rY), Quaternion.identity).transform.SetParent(maptransform);
            }
            else
            {
                i--;
            }
        }
    }
	void SpawnBounds(){
		for (int i = 0; i < boundsHeight * 2; i+=(int)spawnDistance) {
            Instantiate(borderTree, new Vector2((-boundWidth / 2), boundsHeight - i), Quaternion.identity).transform.SetParent(maptransform);
		}
		for (int i = 0; i < boundsHeight * 2; i+=(int)spawnDistance) {
			Instantiate (borderTree, new Vector2 ((boundWidth / 2), boundsHeight - i), Quaternion.identity).transform.SetParent(maptransform);
		}
		for (int i = 0; i < boundWidth; i+=(int)spawnDistance) {
			Instantiate (borderTree, new Vector2 (boundWidth / 2 - i, boundsHeight), Quaternion.identity).transform.SetParent(maptransform);
		}
		for (int i = 0; i < boundWidth / 2; i+=(int)spawnDistance) {
			//Instantiate (exit, new Vector2 (boundWidth - i, -boundsHeight), Quaternion.identity);
			Instantiate (borderTree, new Vector2 (boundWidth / 2 - i, -boundsHeight), Quaternion.identity).transform.SetParent(maptransform);
		}
		exitObj = Instantiate (exit, new Vector2 (0, -boundsHeight + 4f), Quaternion.identity);
        exitObj.GetComponent<exitScript>().followCount = characterNum;
        exitObj.transform.SetParent(maptransform);
        for (int i = (int)boundWidth / 2 + (int)spawnDistance; i < boundWidth; i+=(int)spawnDistance) {
			//Instantiate (exit, new Vector2 (boundWidth - i, -boundsHeight), Quaternion.identity);
			Instantiate (borderTree, new Vector2 (boundWidth / 2 - i, -boundsHeight), Quaternion.identity).transform.SetParent(maptransform);
		}
	}

	void SpawnObstacles(){
		for (int i = 0; i < treeNum; i++) {
			float rX = Random.Range (topLeft.x, topLeft.x + boundWidth);
			float rY = Random.Range (topLeft.y, topLeft.y + boundsHeight * 2) - boundsHeight * 1.5f;
            int a = Random.Range(0, 3);
            float itemChance = Random.Range(0f, 1f);
            Vector2 pos = new Vector2(rX, rY);
            if(Vector2.Distance(pos, exitObj.transform.position) > 14f){
                if (itemChance >= 0f && itemChance <= healthUpChance)
                {
                    Instantiate(items[0], pos, Quaternion.identity).transform.SetParent(maptransform);
                }
                else if(itemChance > healthUpChance && itemChance <= healthUpChance + speedUpChance)
                {
                    Instantiate(items[1], pos, Quaternion.identity).transform.SetParent(maptransform);
                }
                else
                {
                    Instantiate(obstacles[a], pos, Quaternion.identity).transform.SetParent(maptransform);
                }
                
            }
            else
            {
                i--;
            }
           
		}
	}

	void SpawnCharacters(){
		for (int i = 0; i < characterNum; i++) {
			float rX = Random.Range (topLeft.x + boundWidth * .1f, topLeft.x + boundWidth * 0.9f);
			float rY = Random.Range (topLeft.y + boundsHeight * .1f , topLeft.y + boundsHeight * 2 * 0.9f) - boundsHeight * 1.35f;
            Vector2 pos = new Vector2(rX, rY);
            if (Vector2.Distance(pos, new Vector2(0f, 0f)) > 15f)
            {
                Instantiate(character, pos, Quaternion.identity).transform.SetParent(maptransform);
            }
            else
            {
                i--;
            }
        }
	}

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            float rX = Random.Range(topLeft.x, topLeft.x + boundWidth);
            float rY = Random.Range(topLeft.y, topLeft.y + boundsHeight * 2) - boundsHeight * 1.5f;

            Vector2 pos = new Vector2(rX, rY);
            if (Vector2.Distance(pos, new Vector2(0f, 0f)) > 20f)
            {
                Instantiate(enemy, pos, Quaternion.identity).transform.SetParent(maptransform);
            }
            else
            {
                i--;
            }
        }
    }
}
