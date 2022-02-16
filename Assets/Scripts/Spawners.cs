using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public GameObject spawn6;
    public GameObject spawn7;
    public GameObject spawn8;
    public GameObject spawn9;
    public GameObject spawn10;
    public LevelManagerScript lvl;
    public bool hasSpawned;

    void Start()
    {
        lvl = GameObject.Find("LevelManager").GetComponent<LevelManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lvl.timer <= -1f)
        {
            hasSpawned = false;
        }
        if (lvl.timer >= 0f && !hasSpawned)
        {
            hasSpawned = true;
            if (lvl.level == 1)
            {
                Spawn(spawn1);
            }
            if (lvl.level == 2)
            {
                Spawn(spawn2);
            }
            if (lvl.level == 3)
            {
                Spawn(spawn3);
            }
            if (lvl.level == 4)
            {
                Spawn(spawn4);
            }
            if (lvl.level == 5)
            {
                Spawn(spawn5);
            }
            if (lvl.level == 6)
            {
                Spawn(spawn6);
            }
            if (lvl.level == 7)
            {
                Spawn(spawn7);
            }
            if (lvl.level == 8)
            {
                Spawn(spawn8);
            }
            if (lvl.level == 9)
            {
                Spawn(spawn9);
            }
            if (lvl.level == 10)
            {
                Spawn(spawn10);
            }
        }
    }
    void Spawn(GameObject spawntarget)
    {
        Instantiate(spawntarget, this.transform.position, Quaternion.identity);
        lvl.remaining += 1;
    }
}
