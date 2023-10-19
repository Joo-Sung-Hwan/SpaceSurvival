using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Pet
{
    public override void Init()
    {
        petData.speed = 2f;
        player = GameManager.instance.player;
        MagnetPet = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
