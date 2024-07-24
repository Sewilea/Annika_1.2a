using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWarn : MonoBehaviour
{
    public GameObject Prefabtext;
    public GameObject Content;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Warn(string itemname,int amount)
    {
        GameObject text = Instantiate(Prefabtext, Content.transform);
        text.GetComponent<Text>().text = "+" + amount + " " + itemname;
        Destroy(text, 2f);
    }
}
