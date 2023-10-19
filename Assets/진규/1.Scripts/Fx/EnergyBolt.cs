using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : FxManager
{

    public override void Init()
    {
        fd.Attack = 10;
    }

    void Start()
    {
        Init();
    }
}
