using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Enemy
{

    public override void Init()
    {
        ed.name = EnemyType.Alien.ToString();
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
