using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : Weapon
{
    public override void Initalize()
    {
        weaponData.Damage = 10;
    }

    public override void PlayAct(Collider2D collider)
    {
        Enemy e = collider.GetComponent<Enemy>();
        e.Hp -= weaponData.Damage;
        e.CreateDamageTxt(weaponData.Damage);
    }
}
