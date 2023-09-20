using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{

    [HideInInspector] public int Level { get; set; }
    [HideInInspector] public int HP { get; set; }
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
    public BoxCollider area;
    [HideInInspector] public int size;
    public int Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
        }
    }

    float delayTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Size = 2;
        SetAreaSize();
        ps = PlayerState.Idle;
        ani = GetComponent<Animator>();
        Init();
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

    public void SetHP(int hp)
    {
        HP = hp;
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
        SetHP(100);
        SetAttack(10);
        SetDefense(5);
        SetAttackSpeed(1);
        SetSpeed(2);
    }

    public void LaserFire()
    {
        delayTime += Time.deltaTime;
        if (delayTime > 2f)
        {
            GameManager.instance.pollingsystem.PollingLaser(laser, laser_parent);
            delayTime = 0f;
        }
    }
    public void SetAreaSize()
    {
        area.size = new Vector3(Size, Size, Size);
    }
    public Vector2 GetRandomPosition()
    {
        Vector2 pos =   transform.position;
        Vector2 size = area.size;

        float posX = pos.x + UnityEngine.Random.Range(-size.x / 2f, size.x / 2f);
        float posY = pos.y + UnityEngine.Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 spawnPos = new Vector2(posX, posY);
        return spawnPos;
    }
    
    public void CreateBomb()
    {
        delayTime += Time.deltaTime;
        if (delayTime > 2f)
        {
            GameManager.instance.pollingsystem.PollingBomb(bomb, area.transform, GetRandomPosition());

            delayTime = 0f;
        }
    }
}
