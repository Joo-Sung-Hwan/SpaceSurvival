using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<Sprite> idleSp = new List<Sprite>();
    [SerializeField] private List<Sprite> walkSp = new List<Sprite>();
    [SerializeField] private List<Sprite> deadSp = new List<Sprite>();
    public List<Item> items = new List<Item>();

    protected Player player;
    protected EnemyType enemyT = EnemyType.None;
    protected Animator anim;
    protected DefineEnemyData defineData;
    //protected DefineEnemyData defineD = DefineEnemyData.None;


    public abstract void Init();
    public bool IsDead { get; set; }
    public float Hp
    {
        get { return ed.hp; }
        set 
        { 
            ed.hp = value;
            if(ed.hp <= 0)
            {
                Dead();
            }
        }
    }

    public virtual void Move()
    {
        Vector3 distance = player.transform.position - transform.position;
        anim = transform.GetComponent<Animator>();

        transform.Translate(Time.deltaTime * ed.speed * distance.normalized);
        if (distance.normalized.x < 0)
            transform.GetComponent<SpriteRenderer>().flipX = false;
        else if (distance.normalized.x > 0)
            transform.GetComponent<SpriteRenderer>().flipX = true;

        if(ed.enemyState != EnemyState.Walk)
        {
            ed.enemyState = EnemyState.Walk;
            anim.SetTrigger("Walk");
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

    public void Dead()
    {
        IsDead = true;
        ed.enemyState = EnemyState.Dead;
        anim.SetTrigger("Dead");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string str = collision.tag;
        switch(str)
        {
            case "Player":
                GameManager.instance.playerSpawnManager.player.SetHP(ed.attack);
                //Debug.Log(GameManager.instance.playerSpawnManager.player.definePD.CurHp);
                break;
            case "Bomb":
                Hp -= collision.transform.parent.GetComponent<Bomb>().BombAttack;
                break;
        }
    }

    public void ExpCreate()
    {
        Debug.Log("아이템생성");
        int rand = Random.Range (0, 100);
        if(rand < 33)
            GameManager.instance.pollingsystem.PollingItem(items[1], transform);
        else
            GameManager.instance.pollingsystem.PollingItem(items[0], transform);
        gameObject.SetActive(false);
    }
}
