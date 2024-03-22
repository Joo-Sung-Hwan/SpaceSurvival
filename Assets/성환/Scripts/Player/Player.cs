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

    [Header("·¹ÀÌÀú")]
    public Laser laser;
    public Transform laser_parent;

    [Header("ÆøÅº")]
    public Weapon weapon;
    public Bomb bomb;
    public Bomb[] bombkind;
    public BoxCollider2D area;

    [Header("ÃÑ¾Ë")]
    public Weapon bullet;
    public Transform[] bullet_parent;
    [HideInInspector] public int Size { get; set; }

    [Header("¿¡³ÊÁöº¼Æ®")]
    public Weapon energyBolt;
    public List<Transform> enegyTrans;
    public Transform enegy;
    public int index = 0;
    public int count = 0;
    [HideInInspector] public bool levelup;

    float delayTimeL = 0f;
    float delayTimeB = 0f;
    float delayTimeBullet = 0f;
    [HideInInspector] public float BombCTime { get; set; }
    [HideInInspector] public float BulletCTime { get; set; }
    [HideInInspector] public float LaserCTime { get; set; }
    public bool missionCheck = false;
    public Transform missile;
    // Start is called before the first frame update
    void Start()
    {
        //index = 1;
        //levelup = true;
        
        ps = PlayerState.Idle;
        ani = GetComponent<Animator>();
        definePD.MaxHp = 20;
        definePD.CurHp = definePD.MaxHp;
        definePD.MaxExp = 200f;
        definePD.Speed = 2f;
        Init();
        SwitchBombCreate();
        BulletCTime = 2f;
        LaserCTime = 2f;
        GameManager.instance.SetPlayerStatus();
        switch (player_weapon)
        {
            case PlayerWeapon.NormalBomb:
            case PlayerWeapon.MagnetBomb:
            case PlayerWeapon.WebBomb:
            case PlayerWeapon.FireBomb:
                SetBombEuqipment();
                break;
            default:
                break;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.isPause)
        {
            //Fire();
            LevelUp();
            CreateBomb();
            //LaserFire();
            BulletFire();
            Test();
            EnegyBolt();
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
        Dead();
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
                //EnegyBolt();
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
                Weapon b = ObjectPoolSystem.ObjectPoolling<Weapon>.GetPool(bullet, ObjectName.Bullet, bullet_parent[1]);
                b.Initalize();
            }
            else
            {
                Weapon b = ObjectPoolSystem.ObjectPoolling<Weapon>.GetPool(bullet, ObjectName.Bullet, bullet_parent[0]);
                b.Initalize();
            }
            delayTimeBullet = 0f;
        }
    }

    void Test()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            levelup = true;
            index++;
        }
    }
    public void LaserFire()
    {
        delayTimeL += Time.deltaTime;
        if (delayTimeL > LaserCTime)
        {
            Laser l = GameManager.instance.pollingsystem.PollingLaser(laser, laser_parent);
            l.SetData();
            l.CreateLaser();
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
            Weapon weapon = ObjectPoolSystem.ObjectPoolling<Weapon>.GetPool(this.weapon,this.weapon.objectName,transform);
            //Weapon b = GameManager.instance.pollingsystem.PollingBomb(bomb, area.transform);
            weapon.Initalize();
            weapon.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_bomb_parent);
            delayTimeB = 0f;
        }
    }
    public void EnegyBolt()
    {
        if(levelup)
        {
            int val = 360 / index;
            int temVal = val;
            Weapon enegyBolt = ObjectPoolSystem.ObjectPoolling<Weapon>.GetPool(energyBolt, ObjectName.EnergyBolt, enegyTrans[count]);
            enegyBolt.Initalize();

            for (int i = 0; i < index; i++)
            {
                enegyTrans[i].gameObject.SetActive(true);
                enegyTrans[i].rotation = Quaternion.Euler(new Vector3(0f, 0f, temVal));
                temVal += val;
            }
            count++;
        }
        levelup = false;

        enegy.Rotate(Vector3.forward * Time.deltaTime * 70f);
    }

    void Dead()
    {
        if(definePD.CurHp <= 0)
        {
            GameManager.instance.isPause = true;
            GameManager.instance.resultUI.gameObject.SetActive(true);
        }
    }

    public void ChainLighningFire()
    {
        
    }

    public void LevelUp()
    {
        if(definePD.CurExp >= definePD.MaxExp)
        {
            definePD.Level += 1;
            float spareExp = definePD.CurExp - definePD.MaxExp;
            definePD.MaxExp *= 1.2f;
            definePD.CurExp = spareExp;
            GameManager.instance.LevelupPause();
            GameManager.instance.selectCardManager.StartSelectCard();
        }
    }

    void SwitchBombCreate()
    {
        switch (weapon.GetComponent<Bomb>().bt)
        {
            case BombType.Normal:
                BombCTime = 2f;
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
