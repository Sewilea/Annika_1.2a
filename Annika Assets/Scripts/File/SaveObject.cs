using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "Save")]
public class SaveObject : ScriptableObject
{
    public bool LoadReady;
    public string savename;
    public SaveItem[] MyInventory;
    public int Hour, Minute;

}

[System.Serializable]
public class SaveItem
{
    public int ID, TypeID, Amount;
}
