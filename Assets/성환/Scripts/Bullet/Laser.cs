using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Bullet
{
    
    public override void init()
    {
        Speed = 2f;
    }
    public override void SetDir()
    {
        base.SetDir();
    }

    public override void DestroyBullet()
    {
        destroy_time += Time.deltaTime;
        if (destroy_time > 1f)
        {
            gameObject.SetActive(false);
            destroy_time = 0f;
        }
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
        DestroyBullet();
    }
    
}
