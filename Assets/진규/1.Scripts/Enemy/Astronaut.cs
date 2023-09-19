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
    }

    void Start()
    {
        Init();
        DataInPut();
        Debug.Log(ed.exp);
    }

    void Update()
    {
        Move();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void DataInPut()
    {
        base.DataInPut();
    }
}
