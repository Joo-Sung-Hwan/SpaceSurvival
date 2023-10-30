using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_small : Bullet
{
    public override void init()
    {
        Speed = 5f;
        Attack = 10f;
        AttackAbility = 1;
        SetDir();
    }
}
