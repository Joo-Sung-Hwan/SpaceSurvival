using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_small : Bullet
{
    public override void init()
    {
        Speed = 2f;
    }

    public override void Move()
    {
        base.Move();
    }
    void Start()
    {
        init();
    }
}
