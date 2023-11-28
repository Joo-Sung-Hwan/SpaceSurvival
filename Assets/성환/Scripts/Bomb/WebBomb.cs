using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBomb : Bomb
{
    void Start()
    {
        weaponData.Range = 2.5f;
        weaponData.Debuff = 0.25f;
        weaponData.zoneTrans = transform;
        weaponData.collider2D = weaponData.zoneTrans.GetComponent<CircleCollider2D>();
        weaponData.collider2D.radius = weaponData.Range;
        ani = GetComponent<Animator>();
        bt = BombType.Web;
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
        e.webBombZone = true;
        e.DeBuff = weaponData.Debuff;
        e.bombEvent = BombEvent.Web;
        e.Events(this);
    }
}
