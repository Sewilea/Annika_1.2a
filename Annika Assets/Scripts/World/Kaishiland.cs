using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Kaishiland : MonoBehaviour
{
    public bool isfull, isfarm, isdecor;
    public int Objecttype;
    public Inventory Inventory;
    public Item Item;
    public Crop Crop;

    public GameObject Object;
    public SpriteRenderer rd, trd;
    public BoxCollider2D cl;
    public Sprite[] Woods, Stones, Weeds, Fruits, Coals, Irons, Golds;
    public Sprite FarmLand;

    public GameObject tree;
    public int treeint;

    public bool time;
    public float timer;
    public int Grownumber;
    private void Start()
    {
        Inventory = GameObject.Find("Script").GetComponent<Inventory>();
    }

    private void FixedUpdate()
    {
        if(Item == null && !isfarm)
        {
            rd.enabled = false;
            rd.sprite = null;
            cl.enabled = false;
            isfull = false;
            tree.SetActive(false);
        }
        else
        {
            isfull = true;
        }

        if(Item != null)
        {
            if(isfarm && Item.TypeID == 5)
            {
                Growing();
                trd.enabled = true;
            }
            else
            {
                trd.enabled = false;
            }
        }

        if (time)
        {
            timer += 1 * Time.deltaTime;

            if(timer > Crop.GrowSpeed)
            {
                Grownumber++;
                timer = 0;
            }
        }
    }

    public void Growing()
    {
        if (Grownumber < 3)
        {
            trd.sprite = Crop.Grow[Grownumber];
        }

        if (Grownumber >= 3)
        {
            Grownumber = 3;
            trd.sprite = Crop.Grow[Grownumber];
            time = false;
        }
    }

    public void KaishiCreate(int a,Item item)
    {
        if(a == 1)
        {
            rd.enabled = true;
            rd.sprite = Woods[Random.Range(0,Woods.Length)];
            cl.enabled = true;
            Item = item;
        }
        if (a == 2)
        {
            rd.enabled = true;
            rd.sprite = Stones[Random.Range(0, Stones.Length)];
            cl.enabled = true;
            Item = item;
        }
        if (a == 3)
        {
            rd.enabled = true;
            rd.sprite = Weeds[Random.Range(0, Weeds.Length)];
            cl.enabled = true;
            Item = item;
        }
        if (a == 4)
        {
            rd.enabled = true;
            rd.sprite = Woods[2];
            cl.enabled = true;
            tree.SetActive(true);
            Item = item;
        }
        if (a == 5)
        {
            rd.enabled = true;
            rd.sprite = Fruits[0];
            cl.enabled = true;
            Item = item;
        }
        if (a == 6)
        {
            rd.enabled = true;
            rd.sprite = Fruits[1];
            cl.enabled = true;
            Item = item;
        }
        if (a == 7)
        {
            rd.enabled = true;
            rd.sprite = Fruits[2];
            cl.enabled = true;
            Item = item;
        }
        if (a == 8)
        {
            rd.enabled = true;
            rd.sprite = Coals[Random.Range(0, Coals.Length)];
            cl.enabled = true;
            Item = item;
        }
        if (a == 9)
        {
            rd.enabled = true;
            rd.sprite = Irons[Random.Range(0, Irons.Length)];
            cl.enabled = true;
            Item = item;
        }
        if (a == 10)
        {
            rd.enabled = true;
            rd.sprite = Golds[Random.Range(0, Golds.Length)];
            cl.enabled = true;
            Item = item;
        }
    }

    public void KaishiCreate(Item item)
    {
        rd.enabled = true;
        rd.sprite = item.sprite;
        cl.enabled = true;
        Item = item;
    }

    public void CreateFarm()
    {
        rd.enabled = true;
        rd.sprite = FarmLand;
        isfarm = true;
        trd.enabled = false;
    }

    public void DeleteFarm()
    {
        Item = null;
        Grownumber = 0;
        time = false;
        rd.enabled = false;
        isfarm = false;
        rd.sprite = null;
        trd.enabled = false;
    }

}
