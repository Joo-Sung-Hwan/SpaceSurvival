using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct DefinePlayerData
{
    public int Level { get; set; }
    public float CurHp { get; set; }
    public float MaxHp { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float AttackSpeed { get; set; }
    public float Speed { get; set; }
    public float CurExp { get; set; }
    public float MaxExp { get; set; }
}

public class Player : MonoBehaviour
{
    public DefinePlayerData definePD = new DefinePlayerData();
    public PlayerWeapon player_weapon = new PlayerWeapon();

    [HideInInspector] public PlayerState ps;
    Animator ani;

    [Header("������")]
    public Laser laser;
    public Transform laser_parent;

    [Header("��ź")]
    public Bomb bomb;
    public Bomb[] bombkind;
    public BoxCollider2D area;

    [Header("�Ѿ�")]
    public Bullet bullet;
    public Transform[] bullet_parent;
    [HideInInspector] public int Size { get; set; }

    [Header("��������Ʈ")]
    public FxManager fxmanager;
    public List<Transform> enegyTrans;
    public Transform enegy;
    public int index;

    float delayTimeL = 0f;
    float delayTimeB = 0f;
    float delayTimeBullet = 0f;
    [HideInInspector] public float BombCTime { get; set; }
    [HideInInspector] public float BulletCTime { get; set; }
    [HideInInspector] public float LaserCTime { get; set; }






    // Start is called before the first frame update
    void Start()
    {
        player_weapon = PlayerWeapon.NormalBomb;
        SetBombEuqipment();
        ps = PlayerState.Idle;
        ani = GetComponent<Animator>();
        definePD.MaxHp = 100;
        definePD.CurHp = definePD.MaxHp;
        definePD.MaxExp = 200f;
        Init();
        //GameManager.instance.SetPlayerStatus();
        SwitchBombCreate();
        BulletCTime = 2f;
        LaserCTime = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.isPause)
        {
            Fire();
            LevelUp();
            if (ps == PlayerState.Walk)
            {
                ani.ResetTrigger("Idle");
                ani.SetTrigger("Walk");
                ani.Play("walk_side");
            }
            else
            {
                ani.ResetTrigger("Walk");
                ani.SetTrigger("Idle");
            }
        }
        
    }

    public void SetBombEuqipment()
    {
        bomb = bombkind[(int)player_weapon - (int)PlayerWeapon.NormalBomb];
    }
    public void Fire()
    {
        switch (player_weapon)
        {
            case PlayerWeapon.NormalBomb:
            case PlayerWeapon.MagnetBomb:
            case PlayerWeapon.WebBomb:
            case PlayerWeapon.FireBomb:
                CreateBomb();
                break;
            case PlayerWeapon.EnergyBolt:
                EnegyBolt();
                break;
            case PlayerWeapon.Laser:
                LaserFire();
                break;
            default:
                break;
        }
        BulletFire();
    }
    public void Init()
    {
        definePD.Level = 1;
        definePD.Attack = 10;
        definePD.Defense = 10;
        definePD.AttackSpeed = 1;
        definePD.Speed = 2;
    }
    public void BulletFire()
    {
        delayTimeBullet += Time.deltaTime;
        if (delayTimeBullet > BulletCTime)
        {
            if (GetComponent<SpriteRenderer>().flipX)
            {
                GameManager.instance.pollingsystem.PollingBullet(bullet, bullet_parent[1]);
            }
            else
            {
                GameManager.instance.pollingsystem.PollingBullet(bullet, bullet_parent[0]);
            }
            delayTimeBullet = 0f;
        }
    }

    public void LaserFire()
    {
        delayTimeL += Time.deltaTime;
        if (delayTimeL > LaserCTime)
        {
            GameManager.instance.pollingsystem.PollingLaser(laser, laser_parent);
            delayTimeL = 0f;
        }
    }
    public void SetAreaSize(int size)
    {
        Size = size;
        area.size = new Vector3(Size, Size, Size);
    }
    
    public void CreateBomb()
    {
        delayTimeB += Time.deltaTime;
        if (delayTimeB > BombCTime)
        {
            GameManager.instance.pollingsystem.PollingBomb(bomb, area.transform);
            delayTimeB = 0f;
        }
    }
    public void EnegyBolt()
    {
        if (index >= enegyTrans.Count)
        {
            index = enegyTrans.Count;
        }

        foreach (var trans in enegyTrans)
            trans.gameObject.SetActive(false);

        int val = 360 / index;
        int temVal = val;
        for (int i = 0; i < index; i++)
        {
            enegyTrans[i].gameObject.SetActive(true);
            enegyTrans[i].transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, temVal));
            temVal += val;
        }

        foreach (var trans in enegyTrans)
            trans.GetChild(0).rotation = Quaternion.Euler(Vector3.zero);

        enegy.Rotate(Vector3.forward * Time.deltaTime * 70f);
    }

    public void ChainLighningFire()
    {
        
    }

    public void LevelUp()
    {
        if(definePD.CurExp >= definePD.MaxExp)
        {
            float spareExp = definePD.CurExp - definePD.MaxExp;
            definePD.MaxExp *= 1.2f;
            definePD.CurExp = spareExp;
            GameManager.instance.LevelupPause();
            GameManager.instance.selectCardManager.StartSelectCard();
        }
    }

    void SwitchBombCreate()
    {
        switch (bomb.bt)
        {
            case BombType.Nomal:
                BombCTime = 3f;
                break;
            case BombType.Fire:
                BombCTime = 4f;
                break;
            case BombType.Magnet:
            case BombType.Web:
                BombCTime = 6f;
                break;
        }
    }
}
