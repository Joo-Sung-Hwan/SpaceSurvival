using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : Weapon
{
    public float Speed { get; set; }
    //public float Attack { get; set; }
    public int AttackAbility { get; set; }
    [HideInInspector] public int attackability;
    float destroy_time = 0f;
    protected bool dir;

    void Start()
    {
        Speed = 5f;
        weaponData.Damage = 10f;
        AttackAbility = 1;
    }

    public void FixedUpdate()
    {
        if (GameManager.instance.isPause)
            return;
        Attack();
        DestroyBullet();
    }

    public override void Attack()
    {
        if (dir)
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * Speed;
        }
    }

    public void SetDir()
    {
        if (GameManager.instance.player.GetComponent<SpriteRenderer>().flipX)
        {
            dir = false;
        }
        else
        {
            dir = true;
        }
    }

    public void DestroyBullet()
    {
        destroy_time += Time.deltaTime;
        if(destroy_time > 5f)
        {
            ObjectPoolSystem.ObjectPoolling<Weapon>.ReturnPool(this,ObjectName.Bullet);
            destroy_time = 0f;
        }
    }

}
