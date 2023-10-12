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
    public List<EData> eDataList = new List<EData>();


    void Awake() => Instance = this;

    // EData에 EnemyData.csv파일 데이터 입히기
    public void DataSet()
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
    
    // 간소화 함수
    float Cast(List<Dictionary<string, object>> data, int index, string str)
    {
        float dataCast = float.Parse(data[index][str].ToString());
        return dataCast;
    }
}
