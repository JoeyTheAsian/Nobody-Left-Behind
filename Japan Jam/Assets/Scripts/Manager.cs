using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    public Canvas gameOver;

	public float boundsHeight;
	public float boundWidth;
	public List<GameObject> obstacles;
    public GameObject borderTree;
	public GameObject character;
	public GameObject exit;
    public GameObject enemy;
    public GameObject trap;

	public float treeNum;
	public int characterNum;
    public float enemyNum;
    public float trapNum;

	public Vector2 topLeft;
	private float spawnDistance;
    public Transform maptransform;
	// Use this for initialization
	void Start () {
		topLeft = new Vector2 (-boundWidth / 2, boundsHeight / 2);
		spawnDistance = 5f;
		SpawnBounds ();
		SpawnObstacles ();
		SpawnCharacters ();
        SpawnEnemies();
        SpawnTraps();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GameOver()
    {

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
            int a = Random.Range(0, 3);
            Instantiate(trap, new Vector2(rX, rY), Quaternion.identity).transform.SetParent(maptransform);
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
		GameObject exitObj = Instantiate (exit, new Vector2 (0, -boundsHeight + 4f), Quaternion.identity);
        //  exitObj.GetComponent<exitScript>().followCount = characterNum -1 ;
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
            Instantiate(obstacles[a], new Vector2(rX, rY), Quaternion.identity).transform.SetParent(maptransform);
		}
	}

	void SpawnCharacters(){
		for (int i = 0; i < characterNum; i++) {
			float rX = Random.Range (topLeft.x + boundWidth * .1f, topLeft.x + boundWidth * 0.9f);
			float rY = Random.Range (topLeft.y + boundsHeight * .1f , topLeft.y + boundsHeight * 2 * 0.9f) - boundsHeight * 1.35f;
			Instantiate (character, new Vector2 (rX, rY), Quaternion.identity).transform.SetParent(maptransform);
        }
	}

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            float rX = Random.Range(topLeft.x, topLeft.x + boundWidth);
            float rY = Random.Range(topLeft.y, topLeft.y + boundsHeight * 2) - boundsHeight * 1.5f;
            Instantiate(enemy, new Vector2(rX, rY), Quaternion.identity).transform.SetParent(maptransform);
        }
    }
}
