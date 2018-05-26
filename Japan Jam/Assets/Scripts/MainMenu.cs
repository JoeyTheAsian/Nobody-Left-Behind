using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {
    public GameObject Credits;
	// Use this for initialization
	void Start () {
        Credits.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        SceneManager.LoadScene("test");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ShowCredits()
    {
        if (Credits.activeSelf)
        {
            Credits.SetActive(false);
        }
        else
        {
            Credits.SetActive(true);
        }
    }
}
