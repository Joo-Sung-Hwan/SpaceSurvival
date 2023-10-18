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

    [HideInInspector] public PlayerState ps;
    Animator ani;

    [Header("·¹ÀÌÀú")]
    public Laser laser;
    public Transform laser_parent;

    [Header("ÆøÅº")]
    public Bomb bomb;
    public BoxCollider2D area;

    [Header("ÃÑ¾Ë")]
    public Bullet bullet;
    public Transform[] bullet_parent;
    [HideInInspector] public int Size { get; set; }

    [Header("UFO")]
    public GameObject ufo;
    

    float delayTimeL = 0f;
    float delayTimeB = 0f;
    float delayTimeBullet = 0f;
    [HideInInspector] public float BombCTime { get; set; }

   


    // Start is called before the first frame update
    void Start()
    {
        ps = PlayerState.Idle;
        ani = GetComponent<Animator>();
        definePD.MaxHp = 100;
        definePD.CurHp = definePD.MaxHp;
        definePD.MaxExp = 200f;
        Init();
        SetBombCtime(3f);
    }

    // Update is called once per frame
    void Update()
    {
        BulletFire();
        LaserFire();
        CreateBomb();
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

    public void SetLevel(int level)
    {
        definePD.Level = level;
    }

    public void SetHP(float damage)
    {
        definePD.CurHp -= damage;
    }

    public void SetAttack(float attack)
    {
        definePD.Attack = attack;
    }

    public void SetDefense(float defense)
    {
        definePD.Defense = defense;
    }

    public void SetAttackSpeed(float attackspeed)
    {
        definePD.AttackSpeed = attackspeed;
    }

    public void SetSpeed(float speed)
    {
        definePD.Speed = speed;
    }

    public void Init()
    {
        SetLevel(1);
        SetAttack(10);
        SetDefense(5);
        SetAttackSpeed(1);
        SetSpeed(2);
    }
    public void BulletFire()
    {
        delayTimeBullet += Time.deltaTime;
        if(delayTimeBullet > 1f)
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
        if (delayTimeL > 2f)
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

    public void SetBombCtime(float time)
    {
        BombCTime = time;
    }

    public void ChainLighningFire()
    {
        
    }
}
