using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBomb : Bomb
{

    void Start()
    {
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();
        SetBombDebuff(0.25f);
    }

    public override void ResetData()
    {
        base.ResetData();
        bt = BombType.Web;
    }
}
