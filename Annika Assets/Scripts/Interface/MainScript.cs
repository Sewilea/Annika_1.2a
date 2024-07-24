using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public GameObject AraMenü, Console, InfoConsole;
    public bool AraMenüb, Consoleb,InfoConsoleb;
    public AudioSource click;
    public Player player;
    public Inventory inventory;

    [Header("Settings")]
    public KeyCode Menu_code, Console_Code, Info_Code;

    private void Start()
    {
        
    }

    public void OpenPanel(GameObject Thing)
    {
        Thing.SetActive(true);
    }

    public void ClosePanel(GameObject Thing)
    {
        Thing.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(Menu_code))
        {
            AraMenüb = !AraMenüb;
            inventory.ChestPanel.SetActive(false);
            click.Play();
        }

        if (Input.GetKeyDown(Console_Code))
        {
            Consoleb = !Consoleb;
        }

        if (Input.GetKeyDown(Info_Code))
        {
            InfoConsoleb = !InfoConsoleb;
        }

        InfoConsole.SetActive(InfoConsoleb);
        Console.SetActive(Consoleb);
        AraMenü.SetActive(AraMenüb);

    }

    public void _continue()
    {
        AraMenüb = !AraMenüb;
        click.Play();
    }

    public void Click()
    {
        click.Play();
    }


}
