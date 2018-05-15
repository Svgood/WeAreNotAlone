using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : GameEntity
{


    public GameObject projectile;
    int dir = 1;
    bool canShoot = true;
	// Use this for initialization
	void Start () {
        stats = GetComponent<Stats>();
	}
	
    public void init(int dir)
    {
        this.dir = dir;
    }

	// Update is called once per frame
	void Update () {
        statusCheck();
        shoot();
	}

    void shoot()
    {
        if (canShoot)
        {
            var project = Instantiate(projectile, transform.GetChild(0).position, transform.GetChild(0).rotation);
            project.transform.Rotate(new Vector3(0, 0, -90));
            project.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 1200f * dir);
            canShoot = false;
            StartCoroutine(shootCd());
        }
    }

    IEnumerator shootCd()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
