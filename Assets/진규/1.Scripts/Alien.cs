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
        //DataInfo();
        //Debug.Log(ed.exp);
        //Debug.Log(ed.hp);
        //Debug.Log(ed.attack);
        //Debug.Log(ed.defence);
        //Debug.Log(ed.speed);
    }   

    void Update()
    {
        
    }

    public override void DataInfo()
    {
        base.DataInfo();
    }
}
