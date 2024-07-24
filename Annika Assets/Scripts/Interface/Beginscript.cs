using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Beginscript : MonoBehaviour
{
    public GameObject info;
    public GameObject info_own, info_pack;
    public AudioSource click;
    public FileScript FileSc;
    public Button LoadButton;
    public SaveObject Saving;

    private void Start()
    {
        Saving.LoadReady = false;
    }

    private void Update()
    {
        LoadButton.interactable = FileSc.Exits;
    }

    public void LoadBegin()
    {
        Saving.LoadReady = true;
    }
    public void Click()
    {
        click.Play();
    }

    public void _quit()
    {
        Application.Quit();
        click.Play();
    }

    // info

    public void _info_open()
    {
        info.SetActive(true);
        click.Play();
    }

    public void _info_close()
    {
        info.SetActive(false);
        click.Play();
    }

    public void _info_own()
    {
        info_own.SetActive(true);
        info_pack.SetActive(false);
        click.Play();
    }

    public void _info_pack()
    {
        info_own.SetActive(false);
        info_pack.SetActive(true);
        click.Play();
    }

    
}
