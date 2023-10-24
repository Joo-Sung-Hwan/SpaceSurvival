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

    public void ExchangeWeapon(string s)
    {
        switch (s)
        {
            case "��ź":
                player.player_weapon = PlayerWeapon.Bomb;
                break;
            case "������":
                player.player_weapon = PlayerWeapon.Laser;
                break;
            default:
                player.player_weapon = PlayerWeapon.Idle;
                break;
        }
    }

    // �Թ� - ������ player �Ѱ��ֱ�
    public void SetPlayerStatus()
    {
        Dictionary<Enum_GM.abilityName, float> dic_player = new();
        InventoryManager inven = InventoryManager.Instance;
        foreach (var item in inven.d_equipments)
        {
            foreach (var item1 in item.Value.abilities)
            {
                
            }
        }
    }
}
