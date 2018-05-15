using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : GameEntity {

    GameObject target;
    SpriteRenderer sprite;
    Droplist droplist;

    int direction;
    float speed = 0.05f;
    bool canAttack = true;
    bool moving = true;
    bool moveToTarget = true;
    // Use this for initialization
    void Start() {
        droplist = GetComponent<Droplist>();
        target = GameObject.Find("Door");
        stats = GetComponent<Stats>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        movement();
        statusCheck();
        attackCheck();
    }

    //Movement

    void movement()
    {
        if (!moving)
            return;
        updateDir();
        transform.position += Vector3.right * direction * speed;
    }

    void updateDir()
    {
        if (moveToTarget)
        {
            if (target.transform.position.x > transform.position.x)
            {
                direction = 1;
                sprite.flipX = false;
            }
            else
            {
                direction = -1;
                sprite.flipX = true;
            }
        }
    }

    void changeDir()
    {
        moveToTarget = false;
        direction *= -1;
        if (direction == -1)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }


    // Attacks
    void attackCheck()
    {
        if (canAttack)
        {
            foreach (var obj in Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0))
            {
                if (obj.GetComponent<Stats>() != null && obj.tag != "enemy")
                {
                    obj.GetComponent<Stats>().getDmg(stats.diceDmg());
                    moving = false;
                    attack();
                }
                else
                    moving = true;
            }
        }

    }

    void attack()
    {
        
        canAttack = false;
        StartCoroutine(attackCooldown());
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }

    //Override
    override public void onDeathEvent()
    {
        droplist.drop(transform);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "hwall")
            moveToTarget = true;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "dirchange" && moveToTarget)
        {
            changeDir();
        }
    }
}
