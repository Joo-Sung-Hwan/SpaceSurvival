using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardRare : SelectCard
{
    public override void Init()
    {
        gr = Grade.Rare;
    }

    private void Start()
    {
        Init();
    }
}
