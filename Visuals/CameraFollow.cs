using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    // Use this for initialization
    private Vector3 offset;

    void Start()
    {
       offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = (player.transform.position + Mouse.mouseCoords()) / 2 - Vector3.forward * 10;
        transform.position = player.transform.position + offset;
    }
}
