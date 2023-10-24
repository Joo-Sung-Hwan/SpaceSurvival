using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct EnemyData
{
    public string name;
    public float exp;
    public float hp;
    public float attack;
    public float defence;
    public float speed;
    public float magnetStrength;
    public float magnetDistance;
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
    public List<Item> items = new List<Item>();
    public Canvas canvas;
    [SerializeField] TMP_Text damageTxt;

    protected Player player;
    protected EnemyType enemyT = EnemyType.None;
    protected Animator anim;
    protected DefineEnemyData defineData;
    protected Transform magnetTrans;

    float magnetDir = 1f;
    bool looseMagnet = true;
    bool magnetBombZone;
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

    // 플레이어가 바라보는 방향에 몬스터가 플레이어 바라보기
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

    // CSV파일에서 받아온 데이터를 몬스터 이름에 따라 수치 적용하기
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

    // 몬스터 죽었을시 Event 적용
    public void Dead()
    {
        IsDead = true;
        ed.enemyState = EnemyState.Dead;
        anim.SetTrigger("Dead");
    }

    // 오브젝트 Tag에 따라 Trigger 적용 구현
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string str = collision.tag;
        switch(str)
        {
            case "Player":
                GameManager.instance.player.SetHP(ed.attack);
                //Debug.Log(GameManager.instance.playerSpawnManager.player.definePD.CurHp);
                break;
            case "Bomb":
                Hp -= collision.transform.parent.GetComponent<Bomb>().bd.BombAttack;
                CreateDamageTxt(collision.transform.parent.GetComponent<Bomb>().bd.BombAttack);
                break;
            case "Laser":
                Hp -= collision.GetComponent<LaserChild>().Attack;
                CreateDamageTxt(collision.GetComponent<LaserChild>().Attack);
                break;
            case "Bullet":
                Hp -= collision.GetComponent<Bullet>().Attack;
                CreateDamageTxt(collision.GetComponent<Bullet>().Attack);
                break;
            case "EnegyBolt":
                Hp -= collision.GetComponent<FxManager>().fd.Attack;
                CreateDamageTxt(collision.GetComponent<FxManager>().fd.Attack);
                break;
            case "MagnetBomb":
                magnetTrans = collision.transform;
                magnetBombZone = true;
                CreateDamageTxt(collision.transform.parent.GetComponent<Bomb>().bd.BombAttack);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MagnetBomb") && looseMagnet)
        {
            magnetBombZone = false;
        }
    }

    //자석폭탄 이벤트 구현
    public virtual void MagnetEvents()
    {
        if (magnetBombZone)
        {
            Vector2 dirMagnet = magnetTrans.position - transform.position;
            float distance = Vector2.Distance(magnetTrans.position, transform.position);
            float magnetDisStr = (ed.magnetDistance / distance) * ed.magnetStrength;
            transform.Translate((dirMagnet * magnetDir) * magnetDisStr * Time.deltaTime);
        }
    }

    // 데미지 텍스트 구현
    void CreateDamageTxt(float damage)
    {
        Vector3 pos = transform.position + (Vector3.up * 0.5f);
        TMP_Text damageT = Instantiate(damageTxt, pos, Quaternion.identity, canvas.transform);
        damageT.text = damage.ToString();
    }

    // 몬스터가 죽을시 생성되는 아이템
    public void ExpCreate()
    {
        int rand = Random.Range (0, 100);
        int randIndex = rand < 33 ? 1 : 0;
        GameManager.instance.pollingsystem.PollingItem(items[randIndex], transform);
        gameObject.SetActive(false);
    }
}
