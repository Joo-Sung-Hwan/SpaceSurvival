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

    public override void SetDir()
    {
        base.SetDir();
    }

    public override void DestroyBullet()
    {
        base.DestroyBullet();
    }

    void Start()
    {
        init();
    }

    private void OnEnable()
    {
        SetDir();
    }
    private void Update()
    {
        Move();
        DestroyBullet();
    }
}
