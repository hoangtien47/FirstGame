using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class BossController : MonoBehaviour
{
    float horizontal;
    bool isFacingRight;
    Rigidbody2D rb;
    Transform player;
    BossAnimator anim;

    Vector2 target;
    BossCombat cb;
    bool inCb;
    float speed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cb = GetComponent<BossCombat>();
        anim = GetComponent<BossAnimator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isFacingRight = false;
        inCb = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!inCb)
        {
            SetHorizontal();
        }
        else
        {
            horizontal = 0;
            if (GetComponent<BossCombat>().GetForce())
            {
                if (isFacingRight)
                {
                    horizontal = 2;
                }
                else
                {
                    horizontal = -2;
                }
            }
        }
        
        Flip();
        SetPosition();
    }

    void Attack()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        float distance = Mathf.Abs(transform.position.x - player.transform.position.x);
        if (distance < 2f && !inCb)
        {
            anim.SetSpeed(0);
            cb.StarCB();
        }
    }

    public void SetInCB(bool status)
    {
        inCb = status;
    }
    void SetPosition()
    {
        if (!inCb)
        {
            target = new Vector2(player.position.x, transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            anim.SetSpeed(speed);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * 10, 0);
        }
    }
    void SetHorizontal()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        float distance = transform.position.x - player.transform.position.x;
        if (distance > 0.1f)
        {
            horizontal = -1f;
        }
        else if (distance < -0.1f)
        {
            horizontal = 1f;
        }
        else
        {
            horizontal = 0f;
        }
    }
    void Flip()
    {
        if (isFacingRight && horizontal < 0f && !inCb || !isFacingRight && horizontal > 0f && !inCb)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
