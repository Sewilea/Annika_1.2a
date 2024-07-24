using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Sprite[] anim;
    public int x;
    public float hız;
    public bool hızRandom;

    public bool start;
    void Start()
    {
        Invoke("tekrarla", 0);
    }

    
    void Update()
    {
        
    }

    public void tekrarla()
    {
        if (x == anim.Length)
        {
            x = 0;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = anim[x];
        x++;
        Invoke("tekrarla", hız);
    }
}
