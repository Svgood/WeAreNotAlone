using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplist : MonoBehaviour {

    public GameObject[] drops;
    [Range(0, 100)]
    public int dropChance = 25;
	
    public void drop(Transform pos)
    {
        if (diceDropping())
            Instantiate(diceDrop(), pos.position, new Quaternion(0, 0, 0, 0));
    }

    public GameObject diceDrop() => drops[Random.Range(0, drops.Length)];

    public bool diceDropping() => Random.Range(0, 100) < dropChance;


}
