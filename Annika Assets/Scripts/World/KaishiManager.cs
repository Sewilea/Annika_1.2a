using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class KaishiManager : MonoBehaviour
{
    public Inventory Inventory;
    public GameObject Kaishi;
    public List<Vector2> KaishiVectors, CaveVectors;
    public int RandomNumber, CoalNumber, IronNumber, GoldNumber, MobNumber, BeachNumber;
    public Kaishi[] FirstKai, BeachKai;
    public Kaishi[] CaveKai;
    public Kaishi[] UnBannedKai;
    public Kaishi MobKai;
    public GameObject Mob1, Mob2, Mobs;

    [Header("FarmLands")]
    public List<GameObject> Farmland, PlantLand;

    [Header("PerlinNoise")]
    public float seed;
    public float terDetail;
    public float Size_Y;
    public int MobR;


    private void Awake()
    {

    }

    void Start()
    {
        seed = Random.Range(1000, 9999);
        Create();
        CaveCreate();
    }

    void Update()
    {

    }

    void CaveCreate()
    {
        for (int i = 0; i < CaveKai.Length; i++)
        {
            for (float x = CaveKai[i].Xstart; x < CaveKai[i].Xfinish; x++)
            {
                for (float y = CaveKai[i].Ystart; y < CaveKai[i].Yfinish; y++)
                {
                    CaveVectors.Add(new Vector2(x, y));
                }
            }
        }

        for (int i = 0; i < CaveVectors.Count; i++)
        {
            GameObject Area = Instantiate(Kaishi, CaveVectors[i], Quaternion.identity, gameObject.transform);
            Area.name = CaveVectors[i].x + " " + CaveVectors[i].y;
        }

        for (int i = 0; i < CaveVectors.Count; i++)
        {
            Vector2 Vk = CaveVectors[i];
            int maxY = (int)(Mathf.PerlinNoise((Vk.x + seed) / terDetail, (Vk.y + seed) / terDetail) * Size_Y);

            if (maxY < 7)
            {
                float a = Random.Range(0, 10);
                GameObject Obje = GameObject.Find(Vk.x + " " + Vk.y);
                Kaishiland Land = Obje.GetComponent<Kaishiland>();

                if (a < 3)
                {
                    Land.KaishiCreate(2, Inventory.Items[2]);
                }
                if (a == 3)
                {
                    int b = Random.Range(0, CoalNumber);
                    if(b == 0)
                    {
                        Land.KaishiCreate(8, Inventory.Items[10]);
                    }
                }
                if (a == 4)
                {
                    int b = Random.Range(0, IronNumber);
                    if (b == 0)
                    {
                        Land.KaishiCreate(9, Inventory.Items[27]);
                    }
                }
                if (a == 5)
                {
                    int b = Random.Range(0, GoldNumber);
                    if (b == 0)
                    {
                        Land.KaishiCreate(10, Inventory.Items[28]);
                    }
                }
                if(a == 6)
                {
                    float b = Random.Range(0, 3);
                    float c = Random.Range(0, 10);
                    if (b == 0 && c == 0)
                    {
                        Land.KaishiCreate(5, Inventory.Items[7]);
                    }
                    if (b == 1 && c == 0)
                    {
                        Land.KaishiCreate(6, Inventory.Items[8]);
                    }
                    if (b == 2 && c == 0)
                    {
                        Land.KaishiCreate(7, Inventory.Items[9]);
                    }
                }
            }
        }
    }

    public void ReCaveCreate()
    {
        for (int i = 0; i < CaveVectors.Count; i++)
        {
            Vector2 Vk = CaveVectors[i];

            float a = Random.Range(0, 100);
            GameObject Obje = GameObject.Find(Vk.x + " " + Vk.y);
            Kaishiland Land = Obje.GetComponent<Kaishiland>();

            if (a < 3)
            {
                Land.KaishiCreate(2, Inventory.Items[2]);
            }
            if (a == 3)
            {
                int b = Random.Range(0, CoalNumber);
                if (b == 0)
                {
                    Land.KaishiCreate(8, Inventory.Items[10]);
                }
            }
            if (a == 4)
            {
                int b = Random.Range(0, IronNumber);
                if (b == 0)
                {
                    Land.KaishiCreate(9, Inventory.Items[27]);
                }
            }
            if (a == 5)
            {
                int b = Random.Range(0, GoldNumber);
                if (b == 0)
                {
                    Land.KaishiCreate(10, Inventory.Items[28]);
                }
            }
            if (a == 6)
            {
                float b = Random.Range(0, 3);
                float c = Random.Range(0, 10);
                if (b == 0 && c == 0)
                {
                    Land.KaishiCreate(5, Inventory.Items[7]);
                }
                if (b == 1 && c == 0)
                {
                    Land.KaishiCreate(6, Inventory.Items[8]);
                }
                if (b == 2 && c == 0)
                {
                    Land.KaishiCreate(7, Inventory.Items[9]);
                }
            }
        }
        for (float x = BeachKai[0].Xstart; x < BeachKai[0].Xfinish; x++)
        {
            for (float y = BeachKai[0].Ystart; y < BeachKai[0].Yfinish; y++)
            {
                int a = Random.Range(0, BeachNumber);
                int b = Random.Range(0, 2);
                if (a == 0 && b == 0)
                {
                    Inventory.CreateDecor(new Vector2(x, y), Inventory.Items[36]);
                }
                if (a == 0 && b == 1)
                {
                    Inventory.CreateDecor(new Vector2(x, y), Inventory.Items[37]);
                }
            }
        }
        MobCreate();
    }

    void Create()
    {
        for (int i = 0; i < FirstKai.Length; i++)
        {
            for (float x = FirstKai[i].Xstart; x < FirstKai[i].Xfinish; x++)
            {
                for (float y = FirstKai[i].Ystart; y < FirstKai[i].Yfinish; y++)
                {
                    KaishiVectors.Add(new Vector2(x, y));
                }
            }
        }

        for (int i = 0; i < KaishiVectors.Count; i++)
        {
            GameObject Area = Instantiate(Kaishi, KaishiVectors[i], Quaternion.identity, gameObject.transform);
            Area.name = KaishiVectors[i].x + " " + KaishiVectors[i].y;
        }

        for (int i = 0; i < KaishiVectors.Count; i++)
        {
            Vector2 Vk = KaishiVectors[i];

            float a = Random.Range(0, RandomNumber);
            GameObject Obje = GameObject.Find(Vk.x + " " + Vk.y);
            Kaishiland Land = Obje.GetComponent<Kaishiland>();

            if (a < 1)
            {
                float b = Random.Range(0, 7);
                if (b < 1)
                {
                    Land.KaishiCreate(4, Inventory.Items[1]);
                }
            }
            else if (a < 2)
            {
                Land.KaishiCreate(1, Inventory.Items[1]);
            }
            else if (a < 3)
            {
                Land.KaishiCreate(2, Inventory.Items[2]);
            }
            else if (a < 6)
            {
                Land.KaishiCreate(3, Inventory.Items[3]);
            }
            else if (a < 7)
            {
                float b = Random.Range(0, 2);
                float c = Random.Range(0, 10);
                if (b == 0 && c == 0)
                {
                    Land.KaishiCreate(Inventory.Items[31]);
                }
                if (b == 1 && c == 0)
                {
                    Land.KaishiCreate(Inventory.Items[32]);
                }
            }
        }


        for (float x = BeachKai[0].Xstart; x < BeachKai[0].Xfinish; x++)
        {
            for (float y = BeachKai[0].Ystart; y < BeachKai[0].Yfinish; y++)
            {
                int a = Random.Range(0, BeachNumber);
                int b = Random.Range(0, 2);
                if (a == 0 && b == 0)
                {
                    Inventory.CreateDecor(new Vector2(x, y), Inventory.Items[36]);
                }
                if (a == 0 && b == 1)
                {
                    Inventory.CreateDecor(new Vector2(x, y), Inventory.Items[37]);
                }
            }
        }

    }

    public void MobCreate()
    {
        for (float x = MobKai.Xstart; x < MobKai.Xfinish; x++)
        {
            for (float y = MobKai.Ystart; y < MobKai.Yfinish; y++)
            {
                GameObject Obje = GameObject.Find(x + " " + y);
                int a = Random.Range(0, MobNumber);

                if(!Obje.GetComponent<Kaishiland>().isfull)
                {
                    if (a == 0)
                    {
                        Instantiate(Mob1, new Vector2(x, y), Quaternion.identity, Mobs.transform);
                    }
                    if (a == 1)
                    {
                        Instantiate(Mob2, new Vector2(x, y), Quaternion.identity, Mobs.transform);
                    }
                }
               
            }
        }
    }

    public bool InDecorLand(Vector2 Vk)
    {
        for (int i = UnBannedKai[0].Xstart; i < UnBannedKai[0].Xfinish; i++)
        {
            for (int j = UnBannedKai[0].Ystart; j < UnBannedKai[0].Yfinish; j++)
            {
                Vector2 Vk2 = new Vector2(i, j);

                if(Vk == Vk2)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

[System.Serializable]
public class Kaishi
{
    public int Xstart, Xfinish , Ystart, Yfinish;
}
