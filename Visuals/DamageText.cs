using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour {


    float speed = 0.05f;
    Color clr;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.up * speed;
        clr = GetComponent<TextMeshPro>().color;
        clr.a -= 0.02f;
        GetComponent<TextMeshPro>().color = clr;
        if (clr.a <= 0)
            Destroy(this.gameObject);
	}
}
