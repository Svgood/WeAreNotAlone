using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour {


    Player player;
    UI ui;
    public GameObject turret;
    public GameObject wall;
    GameObject curBuilding;

    [HideInInspector]
    public bool startedBuilding = false;
	// Use this for initialization
	void Start () {
        ui = GetComponent<UI>();
        curBuilding = turret;
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        inputCheck();
        buildVisuals();
	}


    void inputCheck()
    {
        //Enable building
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(enable());
        }

        //Change build type
        buildTypeChoose();


        //Build
        if (startedBuilding && Input.GetMouseButtonDown(0))
        {
            if (canBuild())
                build();
            else
                StartCoroutine(enable());
        }

        if (Input.GetKeyDown(KeyCode.Escape) && startedBuilding)
            StartCoroutine(enable());

    }

    void buildTypeChoose()
    {
        if (startedBuilding && Input.GetKeyDown(KeyCode.Alpha1))
        {
            curBuilding = turret;
        }
        if (startedBuilding && Input.GetKeyDown(KeyCode.Alpha2))
        {
            curBuilding = wall;
        }
    }

    bool canBuild()
    {
        if (player.onGround && player.buildDummy.GetComponent<BuildChecker>().canBuild() && player.scrap >= curBuilding.GetComponent<Buildings>().cost)
            return true;
        return false;
    }

    void buildVisuals()
    {
        player.buildDummy.GetComponent<SpriteRenderer>().sprite = curBuilding.GetComponent<SpriteRenderer>().sprite;
        if (canBuild())
        {
            player.buildDummy.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
        }
        else
        {
            player.buildDummy.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        }
    }

    void build()
    {
        player.scrap -= curBuilding.GetComponent<Buildings>().cost;
        int dir = player.transform.position.x > player.buildDummy.transform.position.x ? -1 : 1;
        var obj = Instantiate(curBuilding, player.buildDummy.transform.position, player.buildDummy.transform.rotation);
        if (obj.GetComponent<Turret>() != null)
            obj.GetComponent<Turret>().init(dir);
        StartCoroutine(enable());
    }

    IEnumerator enable()
    {
        yield return new WaitForEndOfFrame();
        startedBuilding = !startedBuilding;
        player.buildDummy.SetActive(!player.buildDummy.activeSelf);
        ui.toggleBuildPanel();
        
    }
}
