using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    public PlayerTest player;
    public Enemy enemy;
    public Transform[] spawnPoint;
    public Transform trans;
    private float spawnTime;


    void Awake() => Instance = this;

    void Start()
    {
        spawnPoint = player.transform.GetChild(0).GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime > 0.2f)
        {
            spawnTime = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        int rand = Random.Range(0, spawnPoint.Length);
        //GameManager.instance.pollingsystem.PollingEnemy(enemy, spawnPoint[rand]);
        Instantiate(enemy, spawnPoint[rand]);
    }
}
