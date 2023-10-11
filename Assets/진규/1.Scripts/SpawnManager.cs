using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Enemy enemy;
    public BoxCollider2D[] spawnPoint;
    private float spawnTime;
    PlayerCamera playerCamera;
    Vector2 spawnspot;

    void Start()
    {
        playerCamera = GameManager.instance.playerCamera;
        spawnPoint = playerCamera.GetComponentsInChildren<BoxCollider2D>();
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

    // PollingSystem을 이용한 Enemy Spawn
    void Spawn()
    {   
        int rand = Random.Range(0, spawnPoint.Length);
        spawnspot = GameManager.instance.GetRandomPosition(spawnPoint[rand].transform, spawnPoint[rand]);
        GameManager.instance.pollingsystem.PollingEnemy(enemy, spawnspot);
    }
}
