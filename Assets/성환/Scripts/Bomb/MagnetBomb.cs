using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBomb : Bomb
{
    public override void Init()
    {
        bt = BombType.Magnet;
        bd.zoneTrans = transform.GetChild(3);
    }

    void Start()
    {
        Init();
        ResetData();
        SetBombAttack(2);
        SetBombSize(1);
    }

    public override void ResetData()
    {
        base.ResetData();
        ani = GetComponent<Animator>();
        AtiveObj(transform,true);
    }
}
