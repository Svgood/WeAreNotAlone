using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildChecker : MonoBehaviour {

    int len;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool canBuild()
    {
        len = 0;
        foreach (var obj in Physics2D.OverlapBoxAll(transform.position, transform.localScale * 0.9f, 0))
            if (obj.GetComponent<Pickable>() == null)
                len += 1;
        if ( len == 1)
            return true;
        return false;
    }
}
