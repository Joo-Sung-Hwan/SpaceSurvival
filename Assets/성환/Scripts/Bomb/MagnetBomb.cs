using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBomb : Bomb
{
    public override void Init()
    {
        bt = BombType.Magnet;
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
        bd.BombAttack = 2f;
    }

    void Start()
    {
        Init();
        ResetData();
    }

    public override void ResetData()
    {
        base.ResetData();
        ani = GetComponent<Animator>();
        AtiveObj(transform,true);
    }
}
