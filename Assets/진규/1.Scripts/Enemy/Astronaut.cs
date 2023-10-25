using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : Enemy
{
    public override void Init()
    {
        ed.name = EnemyType.Astronaut.ToString();
        //ed.exp = (int)DefineEnemyData.Exp;
        //ed.hp = (int)DefineEnemyData.Hp;
        //ed.attack = (int)DefineEnemyData.Attack;
        //ed.defence = (int)DefineEnemyData.Defence;
        //ed.speed = (int)DefineEnemyData.Speed;
        ed.magnetStrength = 1f;
        ed.magnetDistance = 10f;
        player = GameManager.instance.player;
        DataInPut();
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
