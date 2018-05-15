using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour {


    GameObject player;
    Vector3 pStartPos, bgStartPos, offset;
    public float hSpeedDiv = 8, vSpeedDiv = 12;
	// Use this for initialization
	void Start () {
        
        player = GameObject.Find("Player");
        pStartPos = player.transform.position;
        bgStartPos = transform.position;
        offset = bgStartPos - pStartPos;
	}
	
	// Wow so complex formula. Wow
	void Update () {
        transform.position = player.transform.position + Vector3.up * (pStartPos.y - player.transform.position.y)/vSpeedDiv + Vector3.right * (pStartPos.x - player.transform.position.x)/hSpeedDiv + offset;
	}
}
