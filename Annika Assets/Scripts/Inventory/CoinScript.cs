using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public int Coin;
    public Text text;
    Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        text.text = Coin.ToString();
    }

    public void BuyItem(Item item)
    {
        if(Coin >= item.BuyCoin)
        {
            inventory.AddIteminSlots(item, 1);
            Coin = Coin - item.BuyCoin;
        }
    }

    public void SellItem()
    {
        Item item = inventory.Mouse.item;

        if(item.ID != 0 && item.SellCoin != 0)
        {
            Coin = Coin + (item.SellCoin * inventory.Mouse.Amount);
            inventory.Mouse.item = inventory.Empty;
            inventory.Mouse.Amount = 0;
        }

    }
}
