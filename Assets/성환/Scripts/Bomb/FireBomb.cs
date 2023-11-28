using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Bomb
{
    void Start()
    {
        weaponData.Damage = 1f;
        weaponData.Range = 1f;
        weaponData.zoneTrans = transform;
        weaponData.collider2D = weaponData.zoneTrans.GetComponent<CircleCollider2D>();
        weaponData.collider2D.radius = weaponData.Range;
        ani = GetComponent<Animator>();
        bt = BombType.Fire;
        objectName = ObjectName.Bomb;
    }

    public override void Initalize()
    {
        Init();
    }

    public override void PlayAct(Collider2D collider)
    {
        Enemy e = collider.GetComponent<Enemy>();
        e.typeTrans = transform;
        e.fireBombZone = true;
        e.bombEvent = BombEvent.Fire;
        e.Events(this);
    }
}
