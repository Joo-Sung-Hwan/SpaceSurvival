using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp2 : Item
{
    public override void Init()
    {
        info.exp = 20;
    }

    void FixedUpdate()
    {
        base.Magnet();
    }

    public override void Magnet()
    {
        base.Magnet();
    }
}
