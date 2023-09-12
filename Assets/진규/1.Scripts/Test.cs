using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string _exp;
    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("EnemyData");

        for(var i = 0; i < data.Count; i++)
        {
            Debug.Log(data[i]["Name"]+" : "+ data[i]["Exp"] + " : " + data[i]["Hp"] + " : " + data[i]["Attack"] + " : " + data[i]["Defence"] + " : " + data[i]["Speed"]);
        }
    }

}
