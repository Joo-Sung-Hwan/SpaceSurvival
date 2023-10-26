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
    public SpriteRenderer sr;
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

public struct TransType
{
    public Transform magnetTrans;
    public Transform WebTrans;
    public Transform fireTrans;
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
    [SerializeField] TMP_Text damageTxt;
    public EnemyData ed = new EnemyData();
    public TransType transT;
    public List<Item> items = new List<Item>();
    public Canvas canvas;
    Transform targetTrans;
    protected Player player;
    protected EnemyType enemyT = EnemyType.None;
    protected Animator anim;
    protected DefineEnemyData defineData;
    Transform typeTrans;
    //protected Transform magnetTrans;
    //protected Transform webTrans;

    float magnetDir = 1f;
    bool looseZone = true;
    public bool magnetBombZone;
    public bool webBombZone;
    public bool fireBombZone;
    Coroutine coroutine;
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
                fireBombZone = false;
                Dead();
            }
        }
    }
    public float DeBuff { get; set; }

    void Start()
    {
        ed.sr = GetComponent<SpriteRenderer>();
    }

    // 플레이어가 바라보는 방향에 몬스터가 플레이어 바라보기
    public virtual void Move()
    {
        Vector3 distance = player.transform.position - transform.position;
        anim = transform.GetComponent<Animator>();
        if(!webBombZone)
            transform.Translate(Time.deltaTime * ed.speed * distance.normalized);
        else
            transform.Translate(Time.deltaTime * ed.speed * DeBuff * distance.normalized);
        //Debug.Log(ed.speed);
        //Debug.Log(player.bomb.bd.BombDebuff);
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
        StopCoroutine("OnOff");
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
                GameManager.instance.player.definePD.CurHp -= ed.attack;
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
                TransType(collision,out magnetBombZone);
                CreateDamageTxt(collision.transform.parent.GetComponent<Bomb>().bd.BombAttack);
                break;
            case "WebBomb":
                TransType(collision, out webBombZone);
                StartCoroutine("OnOff", collision.gameObject);
                DeBuff = collision.transform.parent.GetComponent<Bomb>().bd.BombDebuff;
                break;
            case "FireBomb":
                TransType(collision, out fireBombZone);
                StartCoroutine("OnOff", collision.gameObject);
                break;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MagnetBomb") && looseZone)
        {
            magnetBombZone = false;
        }
        if(collision.CompareTag("WebBomb") && looseZone)
        {
            webBombZone = false;
            StopCoroutine("OnOff");
            ed.sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    //자석폭탄 이벤트 구현
    public virtual void MagnetEvents()
    {
        if (magnetBombZone)
        {
            Vector2 dirMagnet = typeTrans.position - transform.position;
            float distance = Vector2.Distance(typeTrans.position, transform.position);
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

    // 특정 존의 Transform을 만들어주기위한 코드
    Transform Trans()
    {
        targetTrans = GetComponent<Transform>();
        switch(player.bomb.bt)
        {
            case BombType.Magnet:
                targetTrans = transT.magnetTrans;
                return targetTrans;
            case BombType.Web:
                targetTrans = transT.WebTrans;
                return targetTrans;
            case BombType.Fire:
                targetTrans = transT.fireTrans;
                break;
        }
        return targetTrans;
    }

    void TransType(Collider2D collider, out bool zone)
    {
        typeTrans = Trans();
        typeTrans = collider.transform;
        zone = true;
    }

    public IEnumerator OnOff(GameObject bombObj)
    {
        bool isShow = false;
        Bomb bomb = bombObj.transform.parent.GetComponent<Bomb>();
        for (int i = 0; i < 9; i++)
        {
            if (fireBombZone && !IsDead)
            {
                Hp -= bomb.bd.BombAttack;
                CreateDamageTxt(bomb.bd.BombAttack);
            }
            isShow = !isShow;
            for(int j = 0; j < bomb.colors.Count; j++)
            {
                ed.sr.color = bomb.colors[j];
                yield return new WaitForSeconds(0.2f);
            }
        }
        ed.sr.color = new Color(1f, 1f, 1f, 1f);
    }
}
