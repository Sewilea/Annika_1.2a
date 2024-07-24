using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public bool Timer;
    public float timer;
    public int X;

    public GameObject DialoguePanel, CloseButton;
    public bool DialogueBool;
    public Text Dialoguetext;
    public char[] letters;

    void Start()
    {
        Talk("Hello World!");
    }

    void Update()
    {
        if (Timer)
        {
            timer += 1 * Time.deltaTime;
        }
    }

    public void Talk(string a)
    {
        X = 0;
        Dialoguetext.text = "";
        CloseButton.SetActive(false);
        letters = a.ToCharArray();
        DialoguePanel.SetActive(true);
        DialogueBool = true;
        Invoke("Write", 0.07f);
    }

    public void Write()
    {
        if (X < letters.Length)
        {
            Dialoguetext.text += letters[X].ToString();
            X++;
            Invoke("Write", 0.07f);
        }
        else
        {
            CloseButton.SetActive(true);
        }
    }
}
