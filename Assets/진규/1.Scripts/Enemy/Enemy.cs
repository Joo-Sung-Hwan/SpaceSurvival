using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public struct EnemyData
{
    public string name;
    public float exp;
    public float hp;
    public float attack;
    public float defence;
    public float speed;
    public EnemyState enemyState;
}

public enum EnemyState
{
    Idle,
    Walk,
    Dead
}
public enum EnemyType
{
    None,
    Astronaut,
    Alien
}

public enum DefineEnemyData
{
    Exp,
    Hp,
    Attack,
    Defence,
    Speed
}

public abstract class Enemy : MonoBehaviour
{
    public EnemyData ed = new EnemyData();

    [SerializeField]List<Sprite> walkSp = new List<Sprite>();
    protected Animator anim;
    protected EnemyType enemyT = EnemyType.None;
    protected DefineEnemyData defineData;
    //protected DefineEnemyData defineD = DefineEnemyData.None;


    public abstract void Init();


    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        PlayerTest player = FindObjectOfType<PlayerTest>();
        Vector3 distance = player.transform.position - transform.position;

        transform.Translate(Time.deltaTime * ed.speed * distance.normalized);
        if (distance.normalized.x < 0)
            transform.GetComponent<SpriteRenderer>().flipX = false;
        else if (distance.normalized.x > 0)
            transform.GetComponent<SpriteRenderer>().flipX = true;

        if(ed.enemyState != EnemyState.Walk)
        {
            ed.enemyState = EnemyState.Walk;
            
        }
    }

    public virtual void DataInPut()
    {
        DefineDataEnemy define = DefineDataEnemy.Instance;
        int count = 0;
        while(count < define.eDataList.Count)
        {
            if (ed.name != define.eDataList[count].name)
                count++;
            else
            {
                ed.exp = define.eDataList[count].exp;
                ed.hp = define.eDataList[count].hp;
                ed.attack = define.eDataList[count].attack;
                ed.defence = define.eDataList[count].defence;
                ed.speed = define.eDataList[count].speed;
                break;
            }
        }
    }
}
