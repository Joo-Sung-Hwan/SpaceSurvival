using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBomb : Bomb
{
    public override void Init()
    {
        bt = BombType.Web;
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        Init();
        ResetData();
        SetBombDebuff(0.25f);
    }

    public override void ResetData()
    {
        base.ResetData();
        ani = GetComponent<Animator>();
        AtiveObj(transform, true);
    }
}
