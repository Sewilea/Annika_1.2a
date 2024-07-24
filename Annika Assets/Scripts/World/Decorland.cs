using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorland : MonoBehaviour
{
    public Inventory Inventory;
    public Item Item;

    public SpriteRenderer rd;
    public BoxCollider2D cl;

    public Sprite[] Sprites;
    void Start()
    {
        Inventory = GameObject.Find("Script").GetComponent<Inventory>();
    }

    void Update()
    {
        
    }

    public void DecorCreate(Item item, Sprite sprite)
    {
        Item = item;
        rd.sprite = sprite;
    }
}
