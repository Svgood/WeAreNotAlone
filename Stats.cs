using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float hpMax = 100;
    public float hp = 100;
    public int dmgMin = 6, dmgMax = 14;
    public float armor = 0;

    public GameObject bloodParts;

    public void getDmg(float dmg)
    {
        dmg = dmg - armor < 0 ? 1 : dmg - armor;
        hp -= dmg;

        if (GetComponent<Player>() != null)
        {
            GetComponent<Player>().onDamageEvent(dmg);
        }
        if (GetComponent<Turret>() != null)
        {
            GetComponent<Turret>().onDamageEvent(dmg);
        }
        if (GetComponent<Wall>() != null)
        {
            GetComponent<Wall>().onDamageEvent(dmg);
        }
        if (GetComponent<AI>() != null)
        {
            GetComponent<AI>().onDamageEvent(dmg);
        }
    }

    public void heal(int count)
    {
        hp += count;
        if (hp > hpMax)
            hp = hpMax;
    }

    public int diceDmg() => Random.Range(dmgMin, dmgMax);
}
