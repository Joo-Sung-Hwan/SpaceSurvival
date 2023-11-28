using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBomb : Bomb
{
    void Start()
    {
        bd.BombAttack = 25f;
        bd.BombRange = 1f;
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
        bd.collider2D.radius = bd.BombRange;
        ani = GetComponent<Animator>();
    }


    public override void Initalize()
    {
        bt = BombType.Normal;
        weaponData.createDelay = 2f;
    }
}
