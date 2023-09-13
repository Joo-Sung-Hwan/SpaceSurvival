using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyData
{
    public string name;
    public int exp;
    public int hp;
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
    None,
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
    protected DefineEnemyData defineData = DefineEnemyData.None;
    //protected DefineEnemyData defineD = DefineEnemyData.None;

    public abstract void Init();


    void Update()
    {

    }

    public virtual void DataInfo()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("EnemyData");

        for (int i = 0; i < data.Count; i++)
        {
            if(data[i].ContainsValue(ed.name))
            {
                
                ed.exp = int.Parse(data[i]["Exp"].ToString());
                ed.hp = int.Parse(data[i]["Hp"].ToString());
                ed.attack = float.Parse(data[i]["Attack"].ToString());
                ed.defence = float.Parse(data[i]["Defence"].ToString());
                ed.speed = float.Parse(data[i]["Speed"].ToString());
            }
        }
    }
}
