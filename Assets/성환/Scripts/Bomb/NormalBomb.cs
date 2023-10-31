using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBomb : Bomb
{
    void Start()
    {
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();
        
        bd.BombAttack = 25f;
    }


    public override void ResetData()
    {
        base.ResetData();
        bt = BombType.Nomal;
    }
}
