using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public MainScript mainscript;
    public ItemWarn itemwarn;
    public KaishiManager Manager;
    public Vector2 CursorPosition;
    public GameObject Cursors, ChestPanel;
    public GameObject Player;
    public Player PlayerScript;
    float Distance;

    public Chest ThisChest;
    public List<Slot> Slots, Chest_Slots;
    public Slot Mouse;
    public List<Item> Items;
    public Item Empty;

    public int maxitem;
    public Item Chooseitem;
    public Slot ChooseSlot;
    public int chooseorder;

    public GameObject ItemInfo;
    public Text itemname, iteminfo;

    public TalkBubble Bubble;

    public GameObject Decorland, Parent, CollectItem;

    Skills skills;

    void Start()
    {
        skills = GetComponent<Skills>();
        Bubble = GetComponent<TalkBubble>();

        AddIteminSlots(Items[4], 1);
        AddIteminSlots(Items[21], 1);
        AddIteminSlots(Items[5], 1);
        AddIteminSlots(Items[6], 1);
        
    }

    void Update()
    {
        CursorPosition = Input.mousePosition;
        Cursors.GetComponent<RectTransform>().position = Input.mousePosition;
        

        KeyK();
        if (!mainscript.AraMenüb)
        {
            DecorLand();
            CreateFarmLand();
        }
        
    }

    public Item Find(float id, float typeid)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].ID == id && Items[i].TypeID == typeid)
            {
                return Items[i];
            }
        }
        return Empty;
    }

    void DecorLand()
    {
        if (Input.GetKeyDown(mainscript.Menu_code))
        {
            if(ThisChest != null)
            {
                InventorytoChest();
            }
            else
            {
                ThisChest = null;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            MousePos.x = Mathf.Round(MousePos.x);
            MousePos.y = Mathf.Round(MousePos.y);

            GameObject Land = GameObject.Find(MousePos.x + " " + MousePos.y + "dec");

            if(Land != null)
            {
                Decorland Kai = Land.GetComponent<Decorland>();

                if(Kai.Item.ID == 3 && Kai.Item.TypeID == 4)
                {
                    Chest chest = Land.GetComponent<Chest>();
                    ThisChest = chest;
                    mainscript.AraMenüb = true;
                    ChestPanel.SetActive(true);
                    ChesttoInventory();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Chooseitem != null)
            {
                if (Chooseitem.TypeID == 4)
                {
                    Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    MousePos.x = Mathf.Round(MousePos.x);
                    MousePos.y = Mathf.Round(MousePos.y);

                    GameObject land = GameObject.Find(MousePos.x + " " + MousePos.y);

                    if (GameObject.Find(MousePos.x + " " + MousePos.y + "dec") == null && Manager.InDecorLand(MousePos))
                    {
                        if(land != null)
                        {
                            Kaishiland Kai = land.GetComponent<Kaishiland>();

                            if(!Kai.isfull && !Kai.isfarm)
                            {
                                Kai.isdecor = true;
                                GameObject Obje = Instantiate(Decorland, MousePos, Quaternion.identity, Parent.transform);
                                Obje.name = MousePos.x + " " + MousePos.y + "dec";
                                Decorland decor = Obje.GetComponent<Decorland>();
                                decor.DecorCreate(Chooseitem, Chooseitem.sprite);
                                if(Chooseitem.ID == 3)
                                {
                                    Chest chest = Obje.GetComponent<Chest>();
                                    chest.Establish();
                                }
                                if (Chooseitem.ID == 4)
                                {
                                    Manager.PlantLand.Add(Obje);
                                }
                                DecreaseIteminSlot(ChooseSlot);
                            }
                        }
                        else if(Chooseitem.ID != 4)
                        {
                            GameObject Obje = Instantiate(Decorland, MousePos, Quaternion.identity, Parent.transform);
                            Obje.name = MousePos.x + " " + MousePos.y + "dec";
                            Decorland decor = Obje.GetComponent<Decorland>();
                            decor.DecorCreate(Chooseitem, Chooseitem.sprite);
                            if (Chooseitem.ID == 3)
                            {
                                Chest chest = Obje.GetComponent<Chest>();
                                chest.Establish();
                            }

                            DecreaseIteminSlot(ChooseSlot);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            MousePos.x = Mathf.Round(MousePos.x);
            MousePos.y = Mathf.Round(MousePos.y);

            //Debug.Log(MousePos);

            GameObject Land = GameObject.Find(MousePos.x + " " + MousePos.y + "dec");

            if (Land != null)
            {
                Distance = Vector2.Distance(Player.transform.position, Land.transform.position);
                Decorland Decor = Land.GetComponent<Decorland>();

                if (Distance < 2 && !Player.GetComponent<Player>().Break)
                {
                    
                    if (Land != null &&  Decor.Item.forbreak == Chooseitem)
                    {
                        if (Decor.Item.ID == 3 && Decor.Item.TypeID == 4)
                        {
                            Chest chest = Land.GetComponent<Chest>();
                            if (!chest.IsFull())
                            {
                                AddCollectItem(Decor.Item, 1, MousePos);
                                Destroy(Land);
                                PlayerBreakAnimation();
                                PlayerScript.healt -= 5;
                            }
                        }
                        else
                        {
                            AddCollectItem(Decor.Item, 1,MousePos);
                            Destroy(Land);
                            PlayerBreakAnimation();
                            PlayerScript.healt -= 5;
                        }
                    }

                }
            }
        }
    }

    void CreateFarmLand()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Chooseitem != null)
            {
                if (Chooseitem.TypeID == 3)
                {
                    Bubble.Doyoueat.SetActive(true);
                    Bubble.isTalk = true;
                }

                if(Chooseitem.TypeItem != null)
                {
                    if (Chooseitem.TypeID == 2 && Chooseitem.TypeItem.ID == 4)
                    {
                        PlayerHitAnimation();
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            MousePos.x = Mathf.Round(MousePos.x);
            MousePos.y = Mathf.Round(MousePos.y);

            GameObject Land = GameObject.Find(MousePos.x + " " + MousePos.y);

            if (Land != null)
            {
                Distance = Vector2.Distance(Player.transform.position, Land.transform.position);
                Kaishiland Kaishi = Land.GetComponent<Kaishiland>();
                if (Distance < 2 && !PlayerScript.Break)
                {
                    if(Kaishi.isfarm && Chooseitem.TypeID == 5)
                    {
                        if(Kaishi.Item == null)
                        {
                            Kaishi.Item = Chooseitem;
                            Kaishi.Crop = Chooseitem.ifcrop;
                            DecreaseIteminSlot(ChooseSlot);
                            PlayerBreakAnimation();
                        }
                    }
                    if (Kaishi.Item != null && Kaishi.isfarm && Chooseitem.TypeID == 2)
                    {
                        if (Kaishi.Grownumber == 3)
                        {
                            AddCollectItem(Kaishi.Item.ifcrop.ICrop, Random.Range(1, 4), MousePos);
                            skills.Farming_xp++;
                            Kaishi.Item = null;
                            Kaishi.trd.enabled = false;
                            Kaishi.Grownumber = 0;
                            PlayerCollectAnimation();
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            MousePos.x = Mathf.Round(MousePos.x);
            MousePos.y = Mathf.Round(MousePos.y);

            //Debug.Log(MousePos);

            GameObject Land = GameObject.Find(MousePos.x + " " + MousePos.y);
            
            if(Land != null)
            {
                Distance = Vector2.Distance(Player.transform.position, Land.transform.position);
                Kaishiland Kaishi = Land.GetComponent<Kaishiland>();

                if (Distance < 2 && !Player.GetComponent<Player>().Break)
                {
                    if (Kaishi != null && Chooseitem == Items[6] && !Kaishi.isfarm && !Kaishi.isfull && !Kaishi.isdecor)
                    {
                        Kaishi.CreateFarm();
                        Manager.Farmland.Add(Land);
                        PlayerBreakAnimation();
                        PlayerScript.healt -= 2;
                    }
                    if (Kaishi != null && Chooseitem == Items[5] && Kaishi.isfarm)
                    {
                        Kaishi.DeleteFarm();
                        Manager.Farmland.Remove(Land);
                        PlayerBreakAnimation();
                        PlayerScript.healt -= 2;
                    }
                    if (Kaishi != null && Kaishi.isfull && !Kaishi.isfarm && Kaishi.Item != null && Kaishi.Item.forbreak == Chooseitem && Kaishi.Item.TypeID == 1)
                    {
                        AddCollectItem(Kaishi.Item, Random.Range(1, 4),MousePos );
                        if (Kaishi.tree.activeSelf)
                        {
                            AddCollectItem(Items[15], Random.Range(1, 3), MousePos);
                            AddIteminSlots(Items[26], Random.Range(1, 3));
                        }
                        if(Kaishi.Item.forbreak.ID == 2)
                        {
                            skills.Mining_xp++;
                        }
                        else
                        {
                            skills.Forage_xp++;
                        }
                        Kaishi.Item = null;
                        PlayerBreakAnimation();
                        PlayerScript.healt -= 2;
                        
                    }
                    if (Kaishi != null && Kaishi.Item != null && Kaishi.isfull)
                    {
                        if(Kaishi.Item.TypeID == 3)
                        {
                            AddCollectItem(Kaishi.Item, 1, MousePos);
                            Kaishi.Item = null;
                            PlayerCollectAnimation();
                            PlayerScript.healt -= 2;
                            skills.Forage_xp++;
                        }
                    }
                }
            }

            
        }
    } 

    void PlayerBreakAnimation()
    {
        PlayerScript.isWalk = false;
        PlayerScript.Break = true;
        PlayerScript.AnimationBreak();
    }

    void PlayerCollectAnimation()
    {
        PlayerScript.isWalk = false;
        PlayerScript.Collect = true;
        PlayerScript.AnimationCollect();
    }

    void PlayerHitAnimation()
    {
        PlayerScript.isWalk = false;
        PlayerScript.Collect = true;
        PlayerScript.AnimationHit();
    }

    void KeyK()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            chooseorder = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            chooseorder = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            chooseorder = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            chooseorder = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            chooseorder = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            chooseorder = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            chooseorder = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            chooseorder = 7;
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            chooseorder--;
            if(chooseorder < 0)
            {
                chooseorder = 7;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            chooseorder++;
            if (chooseorder > 7)
            {
                chooseorder = 0;
            }
        }

        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i].isChoose = false;
        }

        Slots[chooseorder].isChoose = true;
        Chooseitem = Slots[chooseorder].item;
        ChooseSlot = Slots[chooseorder];
    }

    // Item

    public void AddIteminSlots(Item item, int Amout)
    {
        itemwarn.Warn(item.ItemName, Amout);
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].item.ID == 0)
            {
                Slots[i].item = item;
                Slots[i].Amount = Amout;
                break;
            }
            else if(Slots[i].item == item)
            {
                Slots[i].Amount = Slots[i].Amount + Amout;
                if (Slots[i].Amount > maxitem)
                {
                    Amout = Slots[i].Amount - maxitem;
                    Slots[i].Amount = maxitem;
                    
                    continue;
                }
                else if (Slots[i].Amount == maxitem)
                {
                    break;
                }
                else
                {
                    break;
                }
            }

        }
    }

    public void DecreaseIteminSlot(Slot slot)
    {
        slot.Amount -= 1;

        if(slot.Amount <= 0)
        {
            Chooseitem = Empty;
        }
    }

    public void AddCollectItem(Item item, int Amout , Vector2 Vk)
    {
        GameObject Obje = Instantiate(CollectItem, Vk, Quaternion.identity, null);
        Obje.GetComponent<CollectItem>().item = item;
        Obje.GetComponent<CollectItem>().Amout = Amout;
    }

    // Chest

    public void ChesttoInventory()
    {
        for (int i = 0; i < Chest_Slots.Count; i++)
        {
            Chest_Slots[i].item = ThisChest.SlotInChest[i].item;
            Chest_Slots[i].Amount = ThisChest.SlotInChest[i].amount;
        }
    }

    public void ChestDelete()
    {
        for (int i = 0; i < Chest_Slots.Count; i++)
        {
            Chest_Slots[i].item = Empty;
            Chest_Slots[i].Image.sprite = null;
            Chest_Slots[i].Amount = 0;
        }
        ThisChest = null;
    }

    public void InventorytoChest()
    {
        for (int i = 0; i < Chest_Slots.Count; i++)
        {
            ThisChest.SlotInChest[i].item = Chest_Slots[i].item;
            ThisChest.SlotInChest[i].amount = Chest_Slots[i].Amount;
        }
        ChestDelete();
    }

    // Craft

    public void Craft(Item need, int amount, Item give, int give_amount)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            Slot slot = Slots[i].GetComponent<Slot>();

            if (slot.item.ID == need.ID && slot.item.TypeID == need.TypeID)
            {
                if (slot.Amount >= amount)
                {
                    slot.Amount -= amount;

                    AddIteminSlots(give, give_amount);
                    break;
                }
            }
        }
    }

    public void Craft_int(int a)
    {
        if(a == 1)
        {
            Craft(Items[1], 5, Items[12], 1);
        }
        if (a == 2)
        {
            Craft(Items[2], 5, Items[13], 1);
        }
        if (a == 3)
        {
            Craft(Items[1], 16, Items[14], 1);
        }
        if (a == 4)
        {
            Craft(Items[27], 4, Items[11], 1);
        }
        if (a == 5)
        {
            Craft(Items[28], 4, Items[22], 1);
        }
        if (a == 6)
        {
            Craft(Items[19], 3, Items[29], 1);
        }
        if (a == 7)
        {
            Craft(Items[23], 2, Items[30], 1);
        }
    }

    // Farm

    public void FarmGrow()
    {
        for (int i = 0; i < Manager.Farmland.Count; i++)
        {
            GameObject Land = Manager.Farmland[i];
            Kaishiland Kai = Land.GetComponent<Kaishiland>();

            Kai.Grownumber++;
        }
    }

    public void PlantGrow()
    {
        for (int i = 0; i < Manager.PlantLand.Count; i++)
        {
            GameObject Land = Manager.PlantLand[i];
            Decorland Kai = Land.GetComponent<Decorland>();

            Vector2 Pos = Land.transform.position;
            GameObject Kaishi = GameObject.Find(Pos.x + " " + Pos.y);
            Kaishi.GetComponent<Kaishiland>().KaishiCreate(4, Items[1]);
            Destroy(Land);
        }
    }

    public void CreateDecor(Vector2 MousePos,Item item)
    {
        GameObject Obje = Instantiate(Decorland, MousePos, Quaternion.identity, Parent.transform);
        Obje.name = MousePos.x + " " + MousePos.y + "dec";
        Decorland decor = Obje.GetComponent<Decorland>();
        decor.DecorCreate(item, item.sprite);
    }
}
