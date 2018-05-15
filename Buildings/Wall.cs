using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : GameEntity
{


    public Sprite img;
    public float hp = 200;
    public float armor = 0;
    float startHp;


	// Use this for initialization
	void Start () {
        stats = GetComponent<Stats>();
        startHp = hp;
        if (img != null)
            GetComponent<SpriteRenderer>().sprite = img;
	}
	
	// Update is called once per frame
	void Update () {
        statusCheck();
	}

}
