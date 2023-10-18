using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardNormal : SelectCard
{
    public override void init()
    {
        gr = Grade.Normal;
    }

    public void Start()
    {
        init();
    }
}
