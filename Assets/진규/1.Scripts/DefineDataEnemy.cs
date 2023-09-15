using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class EData
{
    public string name;
    public float exp;
    public float hp;
    public float attack;
    public float defence;
    public float speed;
}

public class DefineDataEnemy : MonoBehaviour
{
    public static DefineDataEnemy Instance;
    public List<Enemy> enemys = new List<Enemy>();
    public List<EData> eDataList = new List<EData>();

    public TextAsset text;

    void Awake() => Instance = this;

    public void DataInfo()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("EnemyData");

        for (int i = 0; i < data.Count; i++)
        {
            EData _defineData = new EData();
            _defineData.name = data[i]["Name"].ToString();
            _defineData.exp = Cast(data, i, "Exp");
            _defineData.hp = Cast(data, i, "Hp");
            _defineData.attack = Cast(data, i, "Attack");
            _defineData.defence = Cast(data, i, "Defence");
            _defineData.speed = Cast(data, i, "Speed");

            eDataList.Add(_defineData);
        }
    }

    float Cast(List<Dictionary<string, object>> data, int index, string str)
    {
        float dataCast = float.Parse(data[index][str].ToString());
        return dataCast;
    }
}
