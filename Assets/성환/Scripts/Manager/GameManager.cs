using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerSpawnManager playerSpawnManager;
    public PollingSystem pollingsystem;
    public SpawnManager spawnManager;
    public PlayerCamera playerCamera;
    public InGameUI gameUI;
    public bool isPause = false;
    public Player player;
    public SelectCardManager selectCardManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    public Vector2 GetRandomPosition(Transform trans, BoxCollider2D area)
    {

        Vector2 pos = trans.position;
        Vector2 size = area.bounds.size;

        float posX = pos.x + UnityEngine.Random.Range(-size.x / 2f, size.x / 2f);
        float posY = pos.y + UnityEngine.Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 spawnPos = new Vector2(posX, posY);
        return spawnPos;
    }

    public void ReStart()
    {
        isPause = false;
        Time.timeScale = 1;
    }

    public void LevelupPause()
    {
        isPause = true;
    }
}
