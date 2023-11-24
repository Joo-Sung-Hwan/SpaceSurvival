using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ItemDatas
{
    public string name;
    public int rank;
}
public class convertData : MonoBehaviour
{
    ItemDatas itemDatas = new ItemDatas();
    private string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/";
        itemDatas.name = "¤¾¤·";
        itemDatas.rank = 1;
    }
    private void Start()
    {
        string item = JsonUtility.ToJson(itemDatas);
        File.WriteAllText(path + "TestItem", JsonUtility.ToJson(item));
        Debug.Log(path + "TestItem");
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
