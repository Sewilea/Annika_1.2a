using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileScript : MonoBehaviour
{
    Inventory inventory;
    Clock clock;
    public SaveObject Saving;
    public string NameS;
    string pathname, pathName2, pathname3;
    public bool Main, Exits;


    void Start()
    {
        clock = GetComponent<Clock>();
        inventory = GetComponent<Inventory>();
        pathname = "Saves\\" + NameS + "1" + ".txt";
        pathName2 = "Saves\\" + NameS + "2" + ".txt";
        pathname3 = "Saves\\" + NameS + "3" + ".txt";

        if (Saving.LoadReady)
        {
            Load();
        }
    }

    
    void Update()
    {
        if(File.Exists(pathname))
        {
            Exits = true;
        }
        else
        {
            Exits = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && Main)
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.R) && Main)
        {
            Load();
        }
    }

    public void Save()
    {
        string b = "";

        for (int i = 0; i < Saving.MyInventory.Length; i++)
        {
            int ID = inventory.Slots[i].item.ID;
            int TypeID = inventory.Slots[i].item.TypeID;
            int Amount = inventory.Slots[i].Amount;

            b += ID + " " + TypeID + " " + Amount + "v";
        }
        b += Saving.MyInventory.Length;

        File.WriteAllText(pathname, Saving.savename);
        File.WriteAllText(pathName2, b);
        File.WriteAllText(pathname3, clock.Day + " " + clock.Week);

        string readText = File.ReadAllText(pathname);

    }

    public void Load()
    {
        string readText = File.ReadAllText(pathname);
        string readText2 = File.ReadAllText(pathName2);
        string readText3 = File.ReadAllText(pathname3);

        string[] Bb = readText2.Split('v');

        for (int i = 0; i < int.Parse(Bb[Bb.Length - 1]); i++)
        {
            string[] Bi = Bb[i].Split(' ');
            //game.Vk[i] = new Vector2(int.Parse(Bi[0]), int.Parse(Bi[1]));
            Saving.MyInventory[i].ID = int.Parse(Bi[0]);
            Saving.MyInventory[i].TypeID = int.Parse(Bi[1]);
            Saving.MyInventory[i].Amount = int.Parse(Bi[2]);

            int ID = int.Parse(Bi[0]);
            int TypeID = int.Parse(Bi[1]);
            int Amount = int.Parse(Bi[2]);

            Item item = inventory.Find(ID, TypeID);

            inventory.Slots[i].item = item;
            inventory.Slots[i].Amount = Amount;

        }

        string[] Cc = readText3.Split(' ');

        clock.Day = int.Parse(Cc[0]);
        clock.Week = int.Parse(Cc[1]);

        Saving.savename = readText;

        Debug.Log(Saving.savename);
    }
}
