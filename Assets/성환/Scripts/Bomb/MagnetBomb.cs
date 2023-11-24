using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBomb : Bomb
{
    void Start()
    {
        //bd.BombAttack = 2f;
        bd.BombRange = 2f;
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
        bd.collider2D.radius = bd.BombRange;
        ani = GetComponent<Animator>();
    }

    public override void Initalize()
    {
        base.Initalize();
        bt = ObjectName.Magnet;
    }
}
