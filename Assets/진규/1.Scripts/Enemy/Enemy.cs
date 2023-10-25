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

    protected Player player;
    protected EnemyType enemyT = EnemyType.None;
    protected Animator anim;
    protected DefineEnemyData defineData;
    Transform typeTrans;
    protected Transform magnetTrans;
    protected Transform webTrans;

    SpriteRenderer sr;
    float magnetDir = 1f;
    bool looseZone = true;
    bool magnetBombZone;
    public bool webBombZone;
    bool fireBombZone;
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
                Dead();
            }
        }
    }
    public float DeBuff { get; set; }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // �÷��̾ �ٶ󺸴� ���⿡ ���Ͱ� �÷��̾� �ٶ󺸱�
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

    // CSV���Ͽ��� �޾ƿ� �����͸� ���� �̸��� ���� ��ġ �����ϱ�
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

    // ���� �׾����� Event ����
    public void Dead()
    {
        IsDead = true;
        ed.enemyState = EnemyState.Dead;
        anim.SetTrigger("Dead");
    }

    // ������Ʈ Tag�� ���� Trigger ���� ����
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
                TransType(collision);
                CreateDamageTxt(collision.transform.parent.GetComponent<Bomb>().bd.BombAttack);
                break;
            case "WebBomb":
                TransType(collision);
                StartCoroutine(OnOff(collision.gameObject));
                DeBuff = collision.transform.parent.GetComponent<Bomb>().bd.BombDebuff;
                break;
            case "FireBomb":
                TransType(collision);
                StartCoroutine(OnOff(collision.gameObject));
                Hp -= collision.transform.parent.GetComponent<Bomb>().bd.BombAttack;
                break;
                /*switch (bomb.bt)
                {
                    case BombType.Magnet:
                        
                        break;
                    case BombType.Web:
                        
                }
                break;*/
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
            StopAllCoroutines();
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    //�ڼ���ź �̺�Ʈ ����
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

    // ������ �ؽ�Ʈ ����
    void CreateDamageTxt(float damage)
    {
        Vector3 pos = transform.position + (Vector3.up * 0.5f);
        TMP_Text damageT = Instantiate(damageTxt, pos, Quaternion.identity, canvas.transform);
        damageT.text = damage.ToString();
    }

    // ���Ͱ� ������ �����Ǵ� ������
    public void ExpCreate()
    {
        int rand = Random.Range (0, 100);
        int randIndex = rand < 33 ? 1 : 0;
        GameManager.instance.pollingsystem.PollingItem(items[randIndex], transform);
        gameObject.SetActive(false);
    }

    Transform Trans()
    {
        Transform trans = GetComponent<Transform>(); 
        switch(player.bomb.bt)
        {
            case BombType.Magnet:
                trans = transT.magnetTrans;
                magnetBombZone = true;
                return trans;
            case BombType.Web:
                trans = transT.WebTrans;
                webBombZone = true;
                return trans;
            case BombType.Fire:
                trans = transT.fireTrans;
                fireBombZone = true;
                break;
        }
        return trans;
    }

    void TransType(Collider2D collider)
    {
        typeTrans = Trans();
        typeTrans = collider.transform;
    }

    public IEnumerator OnOff(GameObject bombObj)
    {
        bool isShow = false;
        for (int i = 0; i < 9; i++)
        {
            isShow = !isShow;
            sr.color = bombObj.transform.parent.GetComponent<Bomb>().bd.firstColor;
            yield return new WaitForSeconds(0.2f);
            sr.color = bombObj.transform.parent.GetComponent<Bomb>().bd.secondColor;
            yield return new WaitForSeconds(0.2f);
        }
        //sr.color = new Color(1f, 1f, 1f, 1f);
    }
}
