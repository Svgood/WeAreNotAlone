using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    Builder builder;
    Player player;

    float hpBarStartX;
    //UI elems
    public GameObject hpBar;
    public GameObject buildBtn, TurBtn, WBtn;
    public Text ammo, scrap, waveText;

    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
        builder = GetComponent<Builder>();
        hpBarStartX = hpBar.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        updateUI();
	}


    void updateUI()
    {
        ammo.text = player.weapon.ammo_cur + "/" + player.weapon.ammo_max;
        scrap.text = "" + player.scrap;
    }

    public void toggleBuildPanel()
    {
        if (builder.startedBuilding)
        {
            buildBtn.SetActive(false);
            TurBtn.SetActive(true);
            WBtn.SetActive(true);
        }
        else
        {
            buildBtn.SetActive(true);
            WBtn.SetActive(false);
            TurBtn.SetActive(false);
        }
    }

    public void updateHealthBar(float changer)
    {
        changer /= 100;
        hpBar.transform.localScale += Vector3.left * changer;
        hpBar.transform.position += Vector3.left * changer;
    }

    public void showWave(int wave)
    {
        string s = "Wave " + wave;
        waveText.enabled = true;
        StartCoroutine(wavePrint(s));
    }

    IEnumerator wavePrint(string s)
    {
        string showString = "";
        int cursor = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            showString += s[cursor];
            waveText.text = showString;
            cursor++;
            if (cursor >= s.Length)
            {
                StartCoroutine(waveDeprint(s));
                break;
            }
        }
    }

    IEnumerator waveDeprint(string s)
    {
        yield return new WaitForSeconds(1.5f);
        string showString = s;
        int len = showString.Length - 1;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            showString = showString.Substring(0, len);
            len = showString.Length - 1;
            waveText.text = showString;
            if (len == -1)
            {
                waveText.text = "";
                break;
            }
        }
    }
}
