using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    Player player;
    Inventory inventory;
    Clock clock;
    Dialogue dialogue;
    Skills skills;
    public GameObject Player;
    public Text PlayerText, Leveltext;
    public GameObject Massage, Content;
    public InputField field;
    public string[] Command_Words;

    void Start()
    {
        player = Player.GetComponent<Player>();
        inventory = GetComponent<Inventory>();
        clock = GetComponent<Clock>();
        dialogue = GetComponent<Dialogue>();
        skills = GetComponent<Skills>();
    }

    void Update()
    {
        PlayerText.text = "Position \n" +
            "X : " + Mathf.Round(Player.transform.position.x) + " \n" + "Y : " + Mathf.Round(Player.transform.position.y) +
            "\n\nRotation \n" + player.Direction + "\n\nIn your hand : " + inventory.Chooseitem.ItemName + "\n\nSpeed : " + player.speed + " Power : " + player.Power;
        Leveltext.text = "Total XP : " + skills.Total_xp + "\n\nForaging XP : " + skills.Forage_xp + "\nFarming XP : " + skills.Farming_xp + "\nMining XP : " + skills.Mining_xp
            + "\nFighting XP : " + skills.Fighting_xp;
    }

    public void run()
    {
        string Command = field.text;

        Command.TrimEnd();
        Command.ToLower();
        Command_Words = Command.Split(' ');

        if (Command_Words[0] == "*") 
        {
            if (Command_Words[1] == "write") 
            {
                Message(Command_Words[2]);
            }

            if (Command_Words[1] == "help")
            {
                Message("* write x | * tp x y | * give x y z | * camera x | * time x y | * talk x");
            }

            if (Command_Words[1] == "tp")
            {
                int x = int.Parse(Command_Words[2]);
                int y = int.Parse(Command_Words[3]);

                Player.transform.position = new Vector2(x, y);
            }

            if (Command_Words[1] == "give")
            {
                int x = int.Parse(Command_Words[2]);
                int y = int.Parse(Command_Words[3]);
                int amount = int.Parse(Command_Words[4]);

                inventory.AddIteminSlots(inventory.Find(y, x), amount);
            }

            if (Command_Words[1] == "camera")
            {
                int x = int.Parse(Command_Words[2]);

                Camera.main.orthographicSize = x;
            }

            if (Command_Words[1] == "time")
            {
                int x = int.Parse(Command_Words[2]);
                int y = int.Parse(Command_Words[3]);

                clock.Hour = x;
                clock.Minute = y;
            }

            if (Command_Words[1] == "talk")
            {
                dialogue.Talk(Command_Words[2]);
            }

        }
    }

    public void Message(string message)
    {
        GameObject Obje = Instantiate(Massage, Content.transform);
        Obje.transform.GetComponent<Text>().text = message;
        Destroy(Obje, 3f);
        _Content();
    }

    public void _Content()
    {
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Content.transform.childCount * 40);
        // şükür
    }
}
