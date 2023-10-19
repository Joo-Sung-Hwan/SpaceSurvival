using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardUnique : SelectCard
{
    public override void Init()
    {
        gr = Grade.Unique;
    }

    public void Start()
    {
        Init();
    }
}
