using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Bomb
{
    /*void Start()
    {
        bd.BombAttack = 1f;
        bd.BombRange = 1f;
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
        bd.collider2D.radius = bd.BombRange;
        ani = GetComponent<Animator>();
    }*/

    public override void Initalize()
    {
        bt = BombType.Fire;
    }
}
