using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEntity : MonoBehaviour {

    [HideInInspector]
    public Stats stats;
    public GameObject textMesh;

    private void Start()
    {
        
    }

    protected void init()
    {
        stats = GetComponent<Stats>();
    }

    public void statusCheck()
    {
        if (stats.hp <= 0)
            onDeathEvent();
    }

    virtual public void onDeathEvent()
    {
        Destroy(this.gameObject);
    }

    public void onDamageEvent(float dmg)
    {
        damageVisuals(dmg);   
    }

    public void onDamageTextSpawn(float dmg)
    {
        var obj = Instantiate(textMesh, transform.position, new Quaternion(0,0,0,0));
        obj.GetComponent<RectTransform>().position = transform.position;
        obj.GetComponent<TextMeshPro>().text = "-" + dmg;
        obj.transform.position += Vector3.up * transform.localScale.y;
    }

    public void damageVisuals(float dmg)
    {
        Instantiate(stats.bloodParts, transform.position, transform.rotation);
        onDamageTextSpawn(dmg);
    }
}
