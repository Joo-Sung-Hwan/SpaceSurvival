using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Bomb
{
    public override void Init()
    {
        bt = BombType.Fire;
        bd.zoneTrans = transform.GetChild(3);
        bd.firstColor = new Color(243f / 255f, 13f / 255f, 13f / 255f, 1f);
        bd.secondColor = new Color(253 / 255f, 146f / 255f, 138f / 255f, 1f);
    }

    void Start()
    {
        Init();
        ResetData();
        SetBombAttack(1);
        SetBombSize(1);
    }

    public override void ResetData()
    {
        base.ResetData();
        ani = GetComponent<Animator>();
        AtiveObj(transform,true);
    }
}
