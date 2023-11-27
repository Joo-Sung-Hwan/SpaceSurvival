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
}
