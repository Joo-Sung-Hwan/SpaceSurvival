using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBomb : Bomb
{
    void Start()
    {
        weaponData.Damage = 25f;
        weaponData.Range = 1f;
        weaponData.zoneTrans = transform;
        weaponData.collider2D = weaponData.zoneTrans.GetComponent<CircleCollider2D>();
        weaponData.collider2D.radius = weaponData.Range;
        ani = GetComponent<Animator>();
        bt = BombType.Normal;
        objectName = ObjectName.Bomb;
    }

    public override void Initalize()
    {
        weaponData.createDelay = 2f;
        Init();
    }

    public override void PlayAct(Collider2D collider)
    {
        Enemy e = collider.GetComponent<Enemy>();
        e.typeTrans = transform;
        e.normalBombZone = true;
        e.Hp -= weaponData.Damage;
        e.CreateDamageTxt(this.weaponData.Damage);
    }
}
