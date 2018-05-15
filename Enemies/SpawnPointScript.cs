using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{

    public GameObject npc;
    public float interval = 1.5f;
    public float delay_before_spawn = 0f;
    public int spawn_limit = 15;

    int spawn_counter = 0;

    List<GameObject> spawnPoints;
    GameObject enemiesParent;
    WavesController controller;

    
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<WavesController>();
        init();
    }

    public void start(int limit, float intervalTmp)
    {
        spawn_counter = 0;
        spawn_limit = limit;
        interval = intervalTmp;
        StartCoroutine(spawner());
    }


    void init()
    {
        spawnPoints = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i).gameObject);
        }
        enemiesParent = GameObject.Find("Enemies");
    }

    IEnumerator spawner()
    {
        yield return new WaitForSeconds(delay_before_spawn);
        while (true)
        {
            if (spawn_counter >= spawn_limit)
            {
                controller.nextWave();
                break;
            }
            yield return new WaitForSeconds(interval);
            spawn();
        }
    }

    void spawn()
    {
        var spawnPoint = getRandomSpawnPoint();
        Instantiate(npc, enemiesParent.transform).transform.position = spawnPoint.transform.position;
        spawn_counter++;
    }

    GameObject getRandomSpawnPoint() => spawnPoints[Random.Range(0, spawnPoints.Count)];
}
