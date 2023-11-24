using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ItemDatas
{
    public string name;
    public int rank;
    public List<float> damage;
    public List<float> attackSpeed;
}
public class convertData : MonoBehaviour
{
    string path = Application.persistentDataPath + "/";
    ItemDatas itemDatas = new ItemDatas();
    private void Start()
    {
        
    }
    public void SaveItemData(ItemDatas data,string filename)
    {
        File.WriteAllText(path + filename, JsonUtility.ToJson(data));
    }
    public ItemDatas LoadItemData(string filename)
    {
        string data = File.ReadAllText(filename);
        return JsonUtility.FromJson<ItemDatas>(data);
    }
}
