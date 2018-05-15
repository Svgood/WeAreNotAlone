using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Sprite img;
    public Projectile projectile;
    public GameObject recoilFlash;

    public int dmgMin = 40, dmgMax = 50;
    public float cooldown = 0.1f;
    public int ammo_max = 300;
    public int ammo_cur = 150;

    GameObject recoilPoint;
    bool canShoot = true;

    void Start() {
        recoilPoint = transform.GetChild(1).gameObject;
        if (img != null)
            GetComponent<SpriteRenderer>().sprite = img;
    }

    public void shoot()
    {
        if (canShoot && checkMouseDist() && ammo_cur > 0)
        {
            ammo_cur--;
            var project = Instantiate(projectile, transform.GetChild(0).position, transform.GetChild(0).rotation);
            project.transform.Rotate(new Vector3(0, 0, -90));
            project.GetComponent<Rigidbody2D>().AddForce((Mouse.mouseCoords() - transform.GetChild(0).position).normalized * 1000f);
            Instantiate(recoilFlash, recoilPoint.transform.position, transform.rotation);
            canShoot = false;
            StartCoroutine(shootCooldown());
        }
    }

    public void addAmmo(int count)
    {
        ammo_cur += count;
        if (ammo_cur > ammo_max)
            ammo_cur = ammo_max;
    }

    bool checkMouseDist() => Vector3.Distance(Mouse.mouseCoords(), Player.player.transform.position) > 1;

    IEnumerator shootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

}
