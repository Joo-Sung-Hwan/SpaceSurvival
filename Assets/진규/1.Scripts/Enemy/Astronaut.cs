using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : Enemy
{
    public override void Init()
    {
        ed.name = EnemyType.Astronaut.ToString();
        ed.exp = (int)DefineEnemyData.Exp;
        ed.hp = (int)DefineEnemyData.Hp;
        ed.attack = (int)DefineEnemyData.Attack;
        ed.defence = (int)DefineEnemyData.Defence;
        ed.speed = (int)DefineEnemyData.Speed;
        player = GameManager.instance.playerSpawnManager.player;
        DataInPut();
    }

    void Update()
    {
        Debug.Log(player);
        if(player == null)
        {
            player = GameManager.instance.playerSpawnManager.player;
            return;
        }
        Move();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void DataInPut() => base.DataInPut();
}
