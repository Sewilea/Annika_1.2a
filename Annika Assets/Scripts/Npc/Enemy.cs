using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player players;
    GameObject Player;
    SpriteRenderer rd;
    Skills skills;
    Rigidbody2D rb;
    public float Distance;
    public float x, y;
    Vector3 target;
    Vector3 Offset = new Vector3(0, -0.5f, 0);
    public bool Walk;
    public float Heart = 5, Speed = 3;
    void Start()
    {
        players = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>();
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        skills = GameObject.Find("Script").GetComponent<Skills>();
        rd = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("hareket", 0);
    }

    void Update()
    {
        target = Player.transform.position + Offset;
        Distance = Vector2.Distance(target, gameObject.transform.position);

        if(Distance < 7)
        {
            Walk = true;
        }
        else
        {
            Walk = false;
        }

        if (Heart <= 0)
        {
            Destroy(gameObject);
        }

        if (Walk)
        {

            if (gameObject.transform.position.x > target.x)
            {
                x = -1;
            }
            if (gameObject.transform.position.x < target.x)
            {
                x = 1;
            }
            if (gameObject.transform.position.y > target.y)
            {
                y = -1;
            }
            if (gameObject.transform.position.y < target.y)
            {
                y = 1;
            }

            if (x != 0 && x == 1)
            {
                if (target != null && gameObject.transform.position.x < target.x)
                {
                    rb.velocity = new Vector3((Speed * 5) * Time.deltaTime, rb.velocity.y, 0);
                    hareket(x);
                }
            }
            if (x != 0 && x == -1)
            {
                if (target != null && gameObject.transform.position.x > target.x)
                {
                    rb.velocity = new Vector3((-Speed * 5) * Time.deltaTime, rb.velocity.y, 0);
                    hareket(x);
                }
            }
            if (y != 0 && y == 1)
            {
                if (target != null && gameObject.transform.position.y < target.y)
                {
                    rb.velocity = new Vector3(rb.velocity.x, (Speed * 5) * Time.deltaTime, 0);
                    hareket(x);
                }
            }
            if (y != 0 && y == -1)
            {
                if (target != null && gameObject.transform.position.y > target.y)
                {
                    rb.velocity = new Vector3(rb.velocity.x, (-Speed * 5) * Time.deltaTime, 0);
                    hareket(x);
                }
            }

        }
        else
        {
            if (x != 0 && x == 1)
            {
                if (target != null && gameObject.transform.position.x < target.x)
                {
                    rb.velocity = new Vector3((Speed * 5) * Time.deltaTime, 0, 0);
                }
                else if (gameObject.transform.position.x >= target.x)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    x = 0;
                    Invoke("hareket", 1);
                }
            }
            if (x != 0 && x == -1)
            {
                if (target != null && gameObject.transform.position.x > target.x)
                {
                    rb.velocity = new Vector3((-Speed * 5) * Time.deltaTime, 0, 0);
                }
                else if (gameObject.transform.position.x <= target.x)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    x = 0;
                    Invoke("hareket", 1);
                }
            }
            if (y != 0 && y == 1)
            {
                if (target != null && gameObject.transform.position.y < target.y)
                {
                    rb.velocity = new Vector3(0, (Speed * 5) * Time.deltaTime, 0);
                }
                else if (gameObject.transform.position.y >= target.y)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    y = 0;
                    Invoke("hareket", 1);
                }
            }
            if (y != 0 && y == -1)
            {
                if (target != null && gameObject.transform.position.y > target.y)
                {
                    rb.velocity = new Vector3(0, (-Speed * 5) * Time.deltaTime, 0);
                }
                else if (gameObject.transform.position.y <= target.y)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    y = 0;
                    Invoke("hareket", 1);
                }
            }
        }
    }

    public void hareket()
    {
        if (Random.Range(0, 2) == 1)
        {
            int yön = Random.Range(0, 4);
            float yer = Random.Range(1, 6);

            if (yön == 0)
            {
                target = gameObject.transform.position;
                target.x = target.x + yer;
                rd.flipX = true;
                x = 1;
            }
            if (yön == 1)
            {
                target = gameObject.transform.position;
                target.x = target.x - yer;
                rd.flipX = false;
                x = -1;
            }
            if (yön == 2)
            {
                target = gameObject.transform.position;
                target.y = target.y + yer;
                y = 1;
            }
            if (yön == 3)
            {
                target = gameObject.transform.position;
                target.y = target.y - yer;
                y = -1;
            }
        }
        if (x == 0 && y == 0)
        {
            Invoke("hareket", 1);
        }
    }

    public void hareket(float y)
    {
        if (y >= 0)
        {
            rd.flipX = true;
        }
        if (y < 0)
        {
            rd.flipX = false;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "hitcollider" && players.Hit)
        {
            skills.Fighting_xp++;
            Heart--;
            players.Hit = false;
        }
    }
}
