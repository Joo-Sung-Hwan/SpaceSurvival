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
    }

    void Update()
    {
        if (GameManager.instance.isPause)
        {
            GetComponent<Animator>().enabled = false;
            return;
        }
        else
        {
            GetComponent<Animator>().enabled = true;
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
