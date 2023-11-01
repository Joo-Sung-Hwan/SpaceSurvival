using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Pet
{
    public override void Init()
    {
        petData.speed = 2f;
        petData.attack = 2f;
        petData.size = 2;
        petData.buff = 0.25f;
        petData.collider2D = transform.GetChild(0).GetComponent<CircleCollider2D>();
        petData.collider2D.radius = petData.size;
        player = GameManager.instance.player;
        MagnetPet = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
