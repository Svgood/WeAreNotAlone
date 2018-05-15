using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float startForce = 800;
    public float kickForce = 350;
    public int dmgMin = 15, dmgMax = 30;
    public float speed = 600;
    bool enemyHit = false;
    Vector2 dir;

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(Player.player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update () {
        //fly();
	}

    void fly()
    {
        //transform
    }

    void init(Vector2 dir)
    {
        this.dir = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = collision.collider;
        if (col.GetComponent<AI>() != null && !enemyHit)
        {
            enemyHit = true;
            col.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            col.GetComponent<Rigidbody2D>().AddForce((col.transform.position - transform.position) * kickForce);
            col.GetComponent<Stats>().getDmg(diceDmg());
        }
        Destroy(this.gameObject);

    }

    int diceDmg() => Random.Range(dmgMin, dmgMax);
}
