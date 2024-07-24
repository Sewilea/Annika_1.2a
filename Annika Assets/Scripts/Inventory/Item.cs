using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string ItemName, ItemInfo;
    public int ID, TypeID;
    public Sprite sprite;
    public Item TypeItem;

    [Header("Kaishiland")]
    public Item forbreak;
    public Crop ifcrop;
    public int BuyCoin, SellCoin;
}
