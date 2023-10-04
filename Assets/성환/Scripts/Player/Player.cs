using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{

    [HideInInspector] public int Level { get; set; }
    [HideInInspector] public float HP { get; set; }
    [HideInInspector] public float Attack { get; set; }
    [HideInInspector] public float Defense { get; set; }
    [HideInInspector] public float AttackSpeed { get; set; }
    [HideInInspector] public float Speed { get; set; }

    [HideInInspector] public PlayerState ps;
    Animator ani;

    [Header("·¹ÀÌÀú")]
    public Laser laser;
    public Transform laser_parent;

    [Header("ÆøÅº")]
    public Bomb bomb;
    public BoxCollider2D area;
    [HideInInspector] public int Size { get; set; }
    

    float delayTimeL = 0f;
    float delayTimeB = 0f;
    [HideInInspector] public float BombCTime { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        ps = PlayerState.Idle;
        ani = GetComponent<Animator>();
        HP = 100;
        Init();
        SetBombCtime(3f);
    }

    // Update is called once per frame
    void Update()
    {
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
        Level = level;
    }

    public void SetHP(float damage)
    {
        HP -= damage;
    }

    public void SetAttack(float attack)
    {
        Attack = attack;
    }

    public void SetDefense(float defense)
    {
        Defense = defense;
    }

    public void SetAttackSpeed(float attackspeed)
    {
        AttackSpeed = attackspeed;
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public void Init()
    {
        SetLevel(1);
        SetAttack(10);
        SetDefense(5);
        SetAttackSpeed(1);
        SetSpeed(2);
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
}
