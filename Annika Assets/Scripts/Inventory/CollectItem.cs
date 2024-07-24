using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public Item item;
    public int Amout;
    public SpriteRenderer rd;

    void Start()
    {
        rd.sprite = item.sprite;
    }
}
