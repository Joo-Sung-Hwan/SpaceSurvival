using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBomb : Bomb
{
    void Start()
    {
        weaponData.Range = 2f;
        weaponData.zoneTrans = transform;
        weaponData.collider2D = weaponData.zoneTrans.GetComponent<CircleCollider2D>();
        weaponData.collider2D.radius = weaponData.Range;
        ani = GetComponent<Animator>();
        bt = BombType.Magnet;
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
        e.magnetBombZone = true;
        e.bombEvent = BombEvent.Magnet;
        e.Events(this);
    }
}
