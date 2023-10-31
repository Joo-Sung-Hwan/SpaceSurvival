using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : Enemy
{
    public override void Init()
    {
        ed.name = EnemyType.Astronaut.ToString();
        ed.magnetStrength = 1f;
        ed.magnetDistance = 10f;
        player = GameManager.instance.player;
        ed.sr = GetComponent<SpriteRenderer>();
        ed.sr.color = Color.white;
    }

    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            if (player == null)
            {
                player = GameManager.instance.player;
                return;
            }
            if (!IsDead)
            {
                Move();
                MagnetEvents();
            }
        }
    }

    public override void Move()
    {
        base.Move();
    }

    public override void MagnetEvents()
    {
        base.MagnetEvents();
    }

    public override void DataInPut() => base.DataInPut();
}
