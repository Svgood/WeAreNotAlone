using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameEntity
{


    public GameObject projectile;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Builder builder;
    UI ui;
    

    //Checker
    [HideInInspector]
    public bool onGround = false;
    bool canShoot = true;

    //Childs
    [HideInInspector]
    public GameObject gun;
    [HideInInspector]
    public GameObject buildDummy;
    [HideInInspector]
    public Weapon weapon;


    //Stats
    [HideInInspector]
    public float scrap = 1000;

    //Singleton
    public static Player player;

    void Start () {
        player = this;
        gun = transform.GetChild(0).gameObject;
        weapon = gun.GetComponent<Weapon>();

        buildDummy = transform.GetChild(1).gameObject;
        sprite = GetComponent<SpriteRenderer>();
        ui = GetComponent<UI>();
        builder = GetComponent<Builder>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
	}

	void FixedUpdate () {
        inputCheck();
        gravityAdjustment();
        mouseRotation();
        collisionCheck();

        statusCheck();
    }


    //All input
    void inputCheck()
    {

        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            rb.velocity += Vector2.up * 0.2f;
            rb.AddForce(new Vector2(0, 650f));
            onGround = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (rb.velocity.x < 10f)
                rb.AddForce(new Vector2(60, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (rb.velocity.x > -10f)
                rb.AddForce(new Vector2(-60, 0));
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x) > 0)
            {
                rb.velocity -= Vector2.right * (rb.velocity.x / Mathf.Abs(rb.velocity.x)) * 0.3f;
            }
        }


        if (Input.GetMouseButton(0) && !builder.startedBuilding)
        {
            weapon.shoot();
        }

    }



    //Checking on ground and death
    public new void statusCheck()
    {
        if (rb.velocity.y < -0.2f || rb.velocity.y > 0.1f)
            onGround = false;
        else
            onGround = true;
    }


    //Speed up the fall
    void gravityAdjustment()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.down * 0.6f;
        }
    }


    //Hero rotation
    void mouseRotation()
    {
        if (Mouse.mousePos().x > transform.position.x)
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 0);
        else
            transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, 0);

        gunRotation();
    }

    void gunRotation()
    {
        float angle = Vector2.Angle(gun.transform.position, Mouse.mousePos());
        gun.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Mouse.mousePos().y - transform.position.y, Mouse.mousePos().x - gun.transform.position.x) * Mathf.Rad2Deg);
    }


    //What happens when tacking dmg
    public new void onDamageEvent(float dmg)
    {
        ui.updateHealthBar(dmg);
        damageVisuals(dmg);
    }


    //Some physic
    void collisionCheck()
    {
        var objs = Physics2D.OverlapBoxAll(transform.position, transform.localScale * 0.8f, 0);
        foreach (var obj in objs)
            if (obj.GetComponent<Pickable>() != null)
            {
                obj.GetComponent<Pickable>().consume(this);
               
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "hwall" && transform.position.y - collision.transform.position.y >= collision.transform.localScale.y)
        {
            onGround = true;
        }
    }


}
