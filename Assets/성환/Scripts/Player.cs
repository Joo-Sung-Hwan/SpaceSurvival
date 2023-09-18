using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public int Level { get; set; }
    [HideInInspector] public int HP { get; set; }
    [HideInInspector] public float Attack { get; set; }
    [HideInInspector] public float Defense { get; set; }
    [HideInInspector] public float AttackSpeed { get; set; }
    [HideInInspector] public float Speed { get; set; }

    public Bullet[] bullets;
    public Transform bullet_parent;
    float fire_time = 1f;
    float time_D = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
        FireBullet();

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

    public void FireBullet()
    {
        time_D += Time.deltaTime;
        if (time_D > fire_time)
        {
            GameManager.instance.pollingsystem.PollingBullet(bullets[0], bullet_parent);
            time_D = 0f;
        }
    }
}
