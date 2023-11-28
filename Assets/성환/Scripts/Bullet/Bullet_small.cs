using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_small : Bullet
{
    public override void Initalize()
    {
        attackability = AttackAbility;
        SetDir();
    }

    public override void PlayAct(Collider2D collider)
    {
        Enemy e = collider.GetComponent<Enemy>();
        e.Hp -= weaponData.Damage;
    }
}
