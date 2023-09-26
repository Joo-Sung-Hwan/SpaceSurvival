using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Enemy enemy;
    //public Transform trans;
    private Transform[] spawnPoint;
    private float spawnTime;
    Player player;

    void Start()
    {
        player = GameManager.instance.playerSpawnManager.player;
        spawnPoint = player.transform.GetChild(0).GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime > 0.4f)
        {
            spawnTime = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        int rand = Random.Range(1, spawnPoint.Length);
        GameManager.instance.pollingsystem.PollingEnemy(enemy, spawnPoint[rand]);
    }
}
