using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBomb : Bomb
{
    Animator ani;
    public override void Init()
    {
        bt = BombType.Nomal;
        bd.BombAttack = 10f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        ResetData();
        SetBombAttack(50);
        SetBombSize(1);
    }

    public override void ResetData()
    {
        base.ResetData();
        ani = GetComponent<Animator>();
        bs = BombState.Idle;
    }

    // 폭탄 터지는 애니메이션
    public void B_State(BombState bs)
    {
        if (bs == BombState.Idle)
        {
            ani.SetTrigger("idle");
            ani.ResetTrigger("explosion");
            explosion.SetActive(false);
        }
        else
        {
            ani.ResetTrigger("idle");
            ani.SetTrigger("explosion");
            SettingActive(false);
            explosion.SetActive(true);
            GetComponent<Animator>().Play("BombExplosion");
        }
    }
}
