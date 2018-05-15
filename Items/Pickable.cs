using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
        ammo, health, scrap
}

public class Pickable : MonoBehaviour {

    public Type type;
    //public string type = "ammo";
    public int countMin = 10;
    public int countMax = 30;

    public void consume(Player player)
    {
        switch (type)
        {
            case Type.ammo:
                player.weapon.addAmmo(diceCount());
                break;
            case Type.health:
                player.stats.heal(diceCount());
                break;
            case Type.scrap:
                player.scrap += diceCount();
                break;
        }
        Destroy(this.gameObject);
    }

    int diceCount() => Random.Range(countMin, countMax);
    
}
