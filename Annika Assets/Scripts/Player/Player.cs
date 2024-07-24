using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5, Power = 1;
    Animator animator;
    public Yön Direction;
    public bool Idle, Walk, Break, Collect, Hit;
    float x, z;
    public MainScript Main;
    public Inventory inventory;
    public Dialogue dialogue;
    public bool isWalk, istime;

    public float healt;
    public float maxhealt;
    public GameObject healtbar;
    public Text htext;

    public GameObject ColForward, ColBack, ColRight, ColLeft;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Bars();

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        

        if (z != 0)
        {
            if (z > 0.1f)
            {
                Direction = Yön.Top;
            }
            if (z < -0.1f)
            {
                Direction = Yön.Bottom;
            }

        }
        if (x != 0)
        {
            if (x > 0.1f)
            {
                Direction = Yön.Right;
            }
            if (x < -0.1f)
            {
                Direction = Yön.Left;
            }
        }

        if (x == 0 && z == 0)
        {
            Idle = true;
            Walk = false;
        }
        else
        {
            Idle = false;
            Walk = true;
        }

        if (isWalk && !Main.Console.activeSelf && !Main.AraMenü.activeSelf && !inventory.Bubble.isTalk && !dialogue.DialoguePanel.activeSelf)
        {
            istime = true;
            rb.velocity = new Vector2(x * speed, z * speed);
            AnimationWalk();
        }
        else
        {
            rb.velocity = Vector2.zero;
            istime = false;
        }
        
        if(!Break && !Collect)
        {
            AnimationIdle();
        }

        ColliderwithDirection();
    }

    public void ColliderwithDirection()
    {
        if(Direction == Yön.Top)
        {
            ColBack.SetActive(true);
            ColForward.SetActive(false);
            ColRight.SetActive(false);
            ColLeft.SetActive(false);
        }
        if (Direction == Yön.Bottom)
        {
            ColBack.SetActive(false);
            ColForward.SetActive(true);
            ColRight.SetActive(false);
            ColLeft.SetActive(false);
        }
        if (Direction == Yön.Right)
        {
            ColBack.SetActive(false);
            ColForward.SetActive(false);
            ColRight.SetActive(true);
            ColLeft.SetActive(false);
        }
        if (Direction == Yön.Left)
        {
            ColBack.SetActive(false);
            ColForward.SetActive(false);
            ColRight.SetActive(false);
            ColLeft.SetActive(true);
        }
    }

    public void AnimationIdle()
    {
        if (Direction == Yön.Top && Idle)
        {
            animator.Play("IdleBack");
        }
        if (Direction == Yön.Bottom && Idle)
        {
            animator.Play("IdleForward");
        }
        if (Direction == Yön.Right && Idle)
        {
            animator.Play("IdleRight");
        }
        if (Direction == Yön.Left && Idle)
        {
            animator.Play("IdleLeft");
        }
    }

    public void AnimationWalk()
    {
        if (Direction == Yön.Top && Walk)
        {
            animator.Play("WalkBack");
        }
        if (Direction == Yön.Bottom && Walk)
        {
            animator.Play("WalkForward");
        }
        if (Direction == Yön.Right && Walk)
        {
            animator.Play("WalkRight");
        }
        if (Direction == Yön.Left && Walk)
        {
            animator.Play("WalkLeft");
        }
    }

    public void AnimationBreak()
    {
        rb.velocity = Vector2.zero;
        if (Direction == Yön.Top && Break)
        {
            animator.Play("BreakBack");
        }
        if (Direction == Yön.Bottom && Break)
        {
            animator.Play("BreakForward");
        }
        if (Direction == Yön.Right && Break)
        {
            animator.Play("BreakRight");
        }
        if (Direction == Yön.Left && Break)
        {
            animator.Play("BreakLeft");
        }
        Invoke("WalkContinue", 0.4f);
    }

    public void AnimationCollect()
    {
        rb.velocity = Vector2.zero;
        if (Direction == Yön.Top && Collect)
        {
            animator.Play("CollectBack");
        }
        if (Direction == Yön.Bottom && Collect)
        {
            animator.Play("CollectForward");
        }
        if (Direction == Yön.Right && Collect)
        {
            animator.Play("CollectRight");
        }
        if (Direction == Yön.Left && Collect)
        {
            animator.Play("CollectLeft");
        }
        Invoke("WalkContinue", 0.4f);
    }

    public void AnimationHit()
    {
        rb.velocity = Vector2.zero;
        if (Direction == Yön.Top && Collect)
        {
            animator.Play("HitBack");
        }
        if (Direction == Yön.Bottom && Collect)
        {
            animator.Play("HitForward");
        }
        if (Direction == Yön.Right && Collect)
        {
            animator.Play("BreakRight");
        }
        if (Direction == Yön.Left && Collect)
        {
            animator.Play("BreakLeft");
        }
        Invoke("WalkContinue", 0.4f);
        Invoke("HitOpen", 0.1f);
    }

    void WalkContinue()
    {
        isWalk = true;
        Break = false;
        Collect = false;
        Hit = false;
    }

    void HitOpen()
    {
        Hit = true;
        Invoke("HitClose", 0.2f);
    }

    void HitClose()
    {
        Hit = false;
    }

    public void Bars()
    {
        htext.text = Mathf.Round(healt) + " / " + maxhealt;

        healtbar.transform.localScale = new Vector2(1, healt / maxhealt);

        if (healt >= maxhealt)
        {
            healt = maxhealt;
        }

        if (healt < 0)
        {
            healt = 0;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collect")
        {
            CollectItem collect = collision.GetComponent<CollectItem>();
            inventory.AddIteminSlots(collect.item, collect.Amout);
            Destroy(collision.gameObject);
        }
    }

    public enum Yön
    {
        Top,
        Bottom,
        Right,
        Left
    }
}
