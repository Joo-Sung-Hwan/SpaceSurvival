using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardRare : SelectCard
{
    public override void init()
    {
        gr = Grade.Rare;
    }

    private void Start()
    {
        init();
    }
}
