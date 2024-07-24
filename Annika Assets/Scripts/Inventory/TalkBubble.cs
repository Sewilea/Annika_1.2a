using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBubble : MonoBehaviour
{
    public bool isTalk;
    public GameObject Doyoueat, Doyousleep;
    Inventory inventory;
    public KaishiManager manager;
    Player player;
    public GameObject Player;
    public Clock clock;
    void Start()
    {
        inventory = GetComponent<Inventory>();
        player = inventory.PlayerScript;
    }

    void Update()
    {
        
    }

    public void Doyoueatyes()
    {
        inventory.DecreaseIteminSlot(inventory.ChooseSlot);
        player.healt += 20;
        Doyoueat.SetActive(false);
        isTalk = false;
    }

    public void Doyoueatno()
    {
        Doyoueat.SetActive(false);
        isTalk = false;
    }

    public void Doyousleepyes()
    {
        clock.Day++;
        clock.Hour = 6;
        clock.Minute = 0;
        player.healt = 100;
        clock.DayPassed();
        Invoke("AfterDay", 1);
        Doyousleep.SetActive(false);
        isTalk = false;
    }

    public void Doyousleepno()
    {
        Doyousleep.SetActive(false);
        isTalk = false;
    }

    void AfterDay()
    {
        Player.transform.position = new Vector2(0, 14);
        inventory.FarmGrow();
        manager.ReCaveCreate();
        inventory.PlantGrow();
    }
}
