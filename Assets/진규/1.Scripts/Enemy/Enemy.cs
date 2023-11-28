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
    public Transform normalBombTrans;
    public Transform magnetTrans;
    public Transform WebTrans;
    public Transform fireTrans;
}
public enum BombEvent
{
    Magnet,
    Web,
    Fire
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
    [SerializeField] DamageTxt damageTxt;
    public EnemyData ed = new EnemyData();
    public TransType transT;
    public BombEvent bombEvent = new();
    public List<Item> items = new List<Item>();
    public Canvas canvas;
    Transform targetTrans;
    protected Player player;
    protected EnemyType enemyT = EnemyType.None;
    protected Animator anim;
    protected DefineEnemyData defineData;
    public Transform typeTrans;

    float magnetDir = 1f;
    bool looseZone = true;
    [HideInInspector] public bool normalBombZone;
    [HideInInspector] public bool magnetBombZone;
    [HideInInspector] public bool webBombZone;
    [HideInInspector] public bool fireBombZone;

    public SpriteRenderer sr;

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
                canvas.gameObject.SetActive(false);
                fireBombZone = false;
                Dead();
            }
        }
    }
    public float DeBuff { get; set; }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ed.magnetStrength = 1f;
        ed.magnetDistance = 10f;
        DataInPut();
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
        GameManager.instance.gameUI.monsterIndex++;
        StopCoroutine("OnOff");
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
                break;

            case "Laser":
                Hp -= collision.GetComponent<LaserChild>().Attack;
                CreateDamageTxt(collision.GetComponent<LaserChild>().Attack);
                break;
            case "Bullet":
                Hp -= collision.GetComponent<Bullet>().weaponData.Damage;
                //Debug.Log(collision.GetComponent<Bullet>().attackability);
                collision.GetComponent<Bullet>().attackability -= 1;
                //Debug.Log(collision.GetComponent<Bullet>().attackability);
                if (collision.GetComponent<Bullet>().attackability <= 0)
                {
                    collision.gameObject.SetActive(false);
                }
                CreateDamageTxt(collision.GetComponent<Bullet>().weaponData.Damage);
                break;
            /*case "EnegyBolt":
                Hp -= collision.GetComponent<FxManager>().fd.Attack;
                CreateDamageTxt(collision.GetComponent<FxManager>().fd.Attack);
                break;*/
        }
    }

    public void Events(Weapon weapon)
    {
        switch (bombEvent)
        {
            case BombEvent.Magnet:
                MagnetEvents();
                break;
            case BombEvent.Web:
                StartCoroutine("OnOff", weapon);
                break;
            case BombEvent.Fire:
                StartCoroutine("OnOff", weapon);
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
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    //�ڼ���ź �̺�Ʈ ����
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

    public Vector2 Pos()
    {
        Vector3 transformVec = transform.position + (Vector3.up * 0.5f);
        Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(GameManager.instance.canvas.GetComponent<Camera>(), transformVec);
        return screenPoint;
    }

    // ������ �ؽ�Ʈ ����
    public void CreateDamageTxt(float damage)
    {
        Vector3 transformVec = transform.position + (Vector3.up * 0.5f);
        DamageTxt damageT = GameManager.instance.pollingsystem.PoolingDamageTxt(damageTxt, transformVec, canvas);
        damageT.GetComponent<TMP_Text>().text = damage.ToString();
    }

    // ���Ͱ� ������ �����Ǵ� ������
    public void ExpCreate()
    {
        int rand = Random.Range (0, 100);
        int randIndex = rand < 33 ? 1 : 0;
        Item i = GameManager.instance.pollingsystem.PollingItem(items[randIndex], transform);
        i.Init();
        gameObject.SetActive(false);
    }

    // Ư�� ��ź�� ���� ���� ǥ��
    public IEnumerator OnOff(Weapon weapon)
    {
        bool isShow = false;
        Bomb bomb = weapon.GetComponent<Bomb>();
        for (int i = 0; i < 9; i++)
        {
            if (fireBombZone && !IsDead)
            {
                Hp -= bomb.weaponData.Damage;
                CreateDamageTxt(bomb.weaponData.Damage);
            }
            isShow = !isShow;
            for (int j = 0; j < bomb.colors.Count; j++)
            {
                sr.color = bomb.colors[j];
                yield return new WaitForSeconds(0.2f);
            }
        }
        sr.color = Color.white;
    }
}
