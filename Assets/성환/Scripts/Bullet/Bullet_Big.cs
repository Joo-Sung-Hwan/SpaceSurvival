using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Big : Bullet
{
    public override void Initalize()
    {
        Speed = 1f;
    }

    void Start()
    {
        Initalize();
    }
}
