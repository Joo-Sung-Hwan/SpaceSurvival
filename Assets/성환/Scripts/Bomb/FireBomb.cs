using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Bomb
{
    public override void Init()
    {
        bt = BombType.Fire;
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
        //bd.bomb_attack = BombAttack - 19f;
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
