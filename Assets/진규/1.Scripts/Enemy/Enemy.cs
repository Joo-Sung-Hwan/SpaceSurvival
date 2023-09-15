using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public struct EnemyData
{
    public string name;
    public float exp;
    public float hp;
    public float attack;
    public float defence;
    public float speed;
}

public enum EnemyType
{
    None,
    Astronaut,
    Alien
}

public enum DefineEnemyData
{
    Exp,
    Hp,
    Attack,
    Defence,
    Speed
}

public abstract class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public EnemyData ed = new EnemyData();
    public List<Sprite> walkSp = new List<Sprite>();
    protected EnemyType enemyT = EnemyType.None;
    protected DefineEnemyData defineData;
    //protected DefineEnemyData defineD = DefineEnemyData.None;

    void Awake() => Instance = this;

    public abstract void Init();


    void Update()
    {

    }

    List<string> DefineData()
    {
        //List<string> data = new List<string>();
        return  Enum.GetNames(typeof(DefineEnemyData)).ToList();
        
    }

    public void DD()
    {
        List<string> data = new List<string>();
    }
}
