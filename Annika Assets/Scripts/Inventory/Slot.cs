using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour , IPointerDownHandler , IPointerExitHandler , IPointerEnterHandler
{
    public Item item, Empty;
    public int Amount;
    public Text AmountText;
    public Image Image, Choose;
    public bool isChoose;

    Inventory Inventory;

    void Start()
    {
        Inventory = GameObject.Find("Script").GetComponent<Inventory>();
    }

    private void Update()
    {
        Choose.enabled = isChoose;

        if (item.ID != 0)
        {
            Image.enabled = true;
            Image.sprite = item.sprite;
        }
        else
        {
            Image.enabled = false;
            Image.sprite = null;
            Amount = 0;
        }

        if(Amount == 0)
        {
            item = Empty;
            Image.enabled = false;
            Image.sprite = null;
        }
        
        if(Amount > 1)
        {
            AmountText.enabled = true;
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (Inventory.mainscript.AraMenüb)
        {
            if(item == Inventory.Mouse.item)
            {
                Amount += Inventory.Mouse.Amount;
            
                if(Amount == Inventory.maxitem)
                {
                    int Amount2 = Amount;
                    Amount = Inventory.Mouse.Amount;
                    Inventory.Mouse.Amount = Amount2;
                }
                if (Amount > Inventory.maxitem)
                {
                    Inventory.Mouse.Amount = Amount - Inventory.maxitem;
                    Amount = Inventory.maxitem;
                }
                if (Amount < Inventory.maxitem)
                {
                    Inventory.Mouse.Amount = 0;
                    Inventory.Mouse.item = Empty;
                }            
            }
            else
            {
                Item item2 = item;
                item = Inventory.Mouse.item;
                Inventory.Mouse.item = item2;

                int Amount2 = Amount;
                Amount = Inventory.Mouse.Amount;
                Inventory.Mouse.Amount = Amount2;
            }
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (item.ID != 0)
        {
            Inventory.ItemInfo.SetActive(true);
            Inventory.itemname.text = item.ItemName;
            Inventory.iteminfo.text = item.ItemInfo;
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        Inventory.ItemInfo.SetActive(false);
    }
}
