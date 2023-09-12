using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyData
{
    public string name;
    public int exp;
    public int hp;
    public int attack;
    public int defence;
    public int speed;
}

public abstract class Enemy : MonoBehaviour
{
    public EnemyData ed = new EnemyData();
    public List<Sprite> walkSp = new List<Sprite>();

    public abstract void Init();

    // Update is called once per frame
    void Update()
    {
        
    }
}
