using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBomb : Bomb
{
    void Start()
    {
        bd.BombAttack = 1f;
        bd.BombRange = 2.5f;
        bd.BombDebuff = 0.25f;
        bd.zoneTrans = transform.GetChild(2);
        bd.collider2D = bd.zoneTrans.GetComponent<CircleCollider2D>();
        bd.collider2D.radius = bd.BombRange;
        ani = GetComponent<Animator>();
    }
    public override void Initalize()
    {
        base.Initalize();
        bt = ObjectName.Web;
    }
}
