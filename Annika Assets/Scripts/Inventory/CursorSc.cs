using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSc : MonoBehaviour
{
    public Vector2 CursorPosition;
    public GameObject Cursors;
    void Start()
    {
        Cursor.visible = false;
        CursorPosition = Input.mousePosition;
    }

    void Update()
    {
        CursorPosition = Input.mousePosition;
        Cursors.GetComponent<RectTransform>().position = Input.mousePosition;
    }
}
