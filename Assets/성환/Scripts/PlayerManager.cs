using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
    public int Level { get; set; }
    public float MaxHp { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float AttackSpeed { get; set; }
    public float Speed { get; set; }
    public float MaxExp { get; set; }
}

public abstract class PlayerManager : MonoBehaviour , ObectPool
{
    [HideInInspector] public PlayerState playerState;

    private float curExp;
    private float curHp;
    public float CurExp
    {
        get { return curExp; }
        set
        {
            curExp = value;
            if (curExp >= playerData.MaxExp)
            {
                LevelUP();
            }
        }
    }
    public float CurHp
    {
        get { return curHp; }
        set
        {
            curHp = value;
            if (curHp <= 0)
            {
                Dead();
            }
        }
    }
    public PlayerData playerData = new PlayerData();
    private List<Weapon> weapons = new List<Weapon>();
    private Dictionary<Weapon, float> weaponss = new Dictionary<Weapon, float>();
    private Animator ani;
    private ObectPool.PoolsTest<Weapon> pools = new ObectPool.PoolsTest<Weapon>();
    public abstract void Initalize();

    private void Awake()
    {
        ani = GetComponent<Animator>();
        playerState = PlayerState.Idle;
    }
    private void Start()
    {
        Initalize();
        GameManager.instance.SetPlayerStatus();
    }
    private void Update()
    {
        if (!GameManager.instance.isPause)
            return;

        if (weaponss.Count == 0)
            return;

        Fire();
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isPause)
        {
            if (playerState == PlayerState.Walk)
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
    }
    public void SetWeapon(Weapon weapon)
    {
        weaponss.Add(weapon, 0f);
    }
    private void Fire()
    {
        foreach (KeyValuePair<Weapon, float> item in weaponss)
        {
            weaponss[item.Key] += Time.deltaTime;
            if (item.Value >= item.Key.weaponData.createDelay)
            {
                // 무기 생성.
                if (pools.CheckPool())
                {
                    pools.GetPool();
                }
                else
                {
                    //Instantiate(gameObject, transform);
                }
                weaponss[item.Key] = 0f;
            }
        }
    }
    private void LevelUP()
    {
        playerData.Level += 1;
        float spareExp = CurExp - playerData.MaxExp;
        playerData.MaxExp *= 1.2f;
        CurExp = spareExp;
        GameManager.instance.LevelupPause();
        GameManager.instance.selectCardManager.StartSelectCard();
    }
    void Dead()
    {
        GameManager.instance.isPause = true;
        GameManager.instance.resultUI.gameObject.SetActive(true);
    }
}
