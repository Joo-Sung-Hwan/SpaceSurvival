using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
    public int Level { get; set; }
    public float CurHp { get; set; }
    public float MaxHp { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float AttackSpeed { get; set; }
    public float Speed { get; set; }
    public float CurExp { get; set; }
    public float MaxExp { get; set; }
}

public abstract class PlayerManager : MonoBehaviour
{
    [HideInInspector] PlayerState playerState;
    
    public PlayerData playerData = new PlayerData();
    private List<Weapon> weapons = new List<Weapon>();
    private Animator ani;
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
    void FixedUpdate()
    {
        if (!GameManager.instance.isPause)
        {
            //Fire();
            //LevelUp();
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
        Dead();
    }
    private void Fire()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            // 갖고있는 무기들 모두 실행.

            //weapons[i].
        }
    }
    void Dead()
    {
        if (playerData.CurHp <= 0)
        {
            GameManager.instance.isPause = true;
            GameManager.instance.resultUI.gameObject.SetActive(true);
        }
    }
}
