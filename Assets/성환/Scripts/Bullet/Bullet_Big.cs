using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Big : Bullet
{
    public override void Initalize()
    {
        Speed = 1f;
    }

    public override void PlayAct(Collider2D collider)
    {
        throw new System.NotImplementedException();
    }
    void Start()
    {
        Initalize();
    }
}
