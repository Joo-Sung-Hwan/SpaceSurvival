using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spon : MonoBehaviour
{
    public Enemy obj;
    public Transform trans;
    void Start()
    {
        Enemy enemy = Instantiate(obj, trans);
        enemy.Init();
        //enemy.DataInfo(obj.name);
    }

    void Update()
    {
        
    }

    void DataInfo()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("EnemyData");

    }
}
