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

    [HideInInspector] Weapon weapon;
    [HideInInspector] PlayerState ps;
    public List<GameObject> weapon_list = new List<GameObject>();

    

    // Start is called before the first frame update
    void Start()
    {
        ps = PlayerState.Idle;
        Init();
        Instantiate(weapon_list[1], transform);
    }

    // Update is called once per frame
    void Update()
    {

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

    
}
