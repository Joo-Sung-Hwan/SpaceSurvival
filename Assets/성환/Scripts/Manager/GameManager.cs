using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerSpawnManager playerSpawnManager;
    public PollingSystem pollingsystem;
    public SpawnManager spawnManager;
    public PlayerCamera playerCamera;
    public InGameUI gameUI;
    public ResultManager resultUI;
    public bool isPause = false;
    public Player player;
    public SelectCardManager selectCardManager;
    //public WeaponDataManager weaponDataManager;
    public Canvas canvas;

    
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

        Vector2 spawnPos = new Vector2(posX, posY);
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
                player.player_weapon = PlayerWeapon.NormalBomb;
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
        //�������� ������ ���� �Ѱ��ֱ� (���� �������� �ƴϸ� PlayerWeapon.Idle ��ȯ)
        player.player_weapon = InventoryManager.Instance.d_equipments.ContainsKey(Enum_GM.ItemPlace.weapon) ?
        InventoryManager.Instance.d_equipments[Enum_GM.ItemPlace.weapon].itemStaticData.weaponKind : PlayerWeapon.Idle;

        //�������� ������ ability ������ ���� �Ѱ��ֱ� (���� �ʿ�)
        foreach (var item in InventoryManager.Instance.d_totalAb)
        {
            switch (item.Key)
            {
                case Enum_GM.abilityName.damage:
                    player.definePD.MaxHp *= (1 + item.Value * 0.01f);
                    break;
                case Enum_GM.abilityName.range:
                    break;
                case Enum_GM.abilityName.attackSpeed:
                    break;
                case Enum_GM.abilityName.speed:
                    break;
                default:
                    break;
            }      
        }
    }
}
