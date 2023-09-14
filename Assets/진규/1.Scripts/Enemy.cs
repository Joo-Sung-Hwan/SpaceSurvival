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
    public EnemyData ed = new EnemyData();
    public List<Sprite> walkSp = new List<Sprite>();
    protected EnemyType enemyT = EnemyType.None;
    protected DefineEnemyData defineData;
    //protected DefineEnemyData defineD = DefineEnemyData.None;

    public abstract void Init();


    void Update()
    {

    }

    public virtual void DataInfo()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("EnemyData");
        //List<float> defineDs = DefineData();
        for (int i = 0; i < data.Count; i++)
        {
            if(data[i].ContainsValue(ed.name))
            {
                ed.exp = Cast(data,i,"Exp");
                ed.hp = Cast(data,i,"Hp");
                ed.attack = Cast(data,i, "Attack");
                ed.defence = Cast(data,i, "Defence");
                ed.speed = Cast(data,i, "Speed");
                //ed.exp = int.Parse(data[i]["Exp"].ToString());
                //ed.hp = int.Parse(data[i]["Hp"].ToString());
                //ed.attack = float.Parse(data[i]["Attack"].ToString());
                //ed.defence = float.Parse(data[i]["Defence"].ToString());
                //ed.speed = float.Parse(data[i]["Speed"].ToString());
            }
        }
    }

    float Cast(List<Dictionary<string, object>> data, int index, string str)
    {
        int dataCast = int.Parse(data[index][str].ToString());
        return dataCast;
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
