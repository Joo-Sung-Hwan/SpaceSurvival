using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardNormal : SelectCard
{
    public override void Init()
    {
        gr = Grade.Normal;
    }

    public void Start()
    {
        Init();
    }
}
