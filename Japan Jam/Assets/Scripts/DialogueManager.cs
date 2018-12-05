using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {
    
    float charTimer;
    public float charTime;

    public playerControl player;
    public Text dialogueBox;
    List<string> dialogue = new List<string>();
    Queue<char> displayText = new Queue<char>();
    bool textScrolling = false;
    // Use this for initialization
    void Start () {
        charTimer = charTime;
	}
    public void AddDialogue(string s)
    {
        dialogue.Add(s);
    }
    public void AddDialogue(string[] stringList)
    {
        foreach (string s in stringList) {
            dialogue.Add(s);
        }
    }
	public void ShowText(string s)
    {
        char[] text = s.ToCharArray();
        foreach(char c in text)
        {
            displayText.Enqueue(c);
        }
        charTimer = charTime;
        textScrolling = true;
        Time.timeScale = 0f;
    }
	// Update is called once per frame
	void Update () {
        if (textScrolling)
        {
            charTimer -= Time.unscaledDeltaTime;
            if (displayText.Count == 0)
            {
                textScrolling = false;
            }
            if (charTimer <= 0f)
            {
                for(int i = 0; i <=Mathf.Abs(charTimer/charTime); i++)
                {
                   dialogueBox.text += displayText.Dequeue();
                }
                charTimer += charTime;
            }
        }
	}
    public void NextText()
    {
        if(dialogue.Count <= 0)
        {
            HideText();
        }
        else if(displayText.Count <= 0)
        {
            ClearText();
            ShowText(dialogue[0]);
            dialogue.Remove(dialogue[0]);
        }
        else
        {
            while(displayText.Count > 0)
            {
                dialogueBox.text += displayText.Dequeue();
            }
        }
    }
    public void ClearText()
    {
        dialogueBox.text = "";
    }
    public void HideText()
    {
        if (!player.paused)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
