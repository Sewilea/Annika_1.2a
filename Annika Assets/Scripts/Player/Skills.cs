using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public int Total_xp;
    public int Forage_xp, Mining_xp, Fighting_xp, Farming_xp;
    
    void Update()
    {
        Total_xp = Forage_xp + Mining_xp + Fighting_xp + Farming_xp;
    }
}
