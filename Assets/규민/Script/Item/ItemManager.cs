using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class ItemData
{
    public string name;
    public string place;
    public string spriteName;
    public string description;

    public ItemData(string name, string place, string spriteName, string description)
    {
        this.name = name;
        this.place = place;
        this.spriteName = spriteName;
        this.description= description;
    }
}

public class ItemManager : MonoBehaviour
{
    public TextAsset jsonFile;

    [HideInInspector] public List<ItemData> items = new List<ItemData>();

    // Start is called before the first frame update
    void Start()
    {
        LoadItem();

        string jItems = JsonConvert.SerializeObject(items);
        Debug.Log(jItems);
    }

    private void Update()
    {

    }

    public void SaveItem()
    {
        string jitems = JsonConvert.SerializeObject(items , Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Items.json" , jitems);
        //File.WriteAllText(jsonFile.text, jitems);
    }

    public void LoadItem()
    { 
        //string jitem = File.ReadAllText(Application.dataPath + "/Items.json");
        List <ItemData> testring = JsonConvert.DeserializeObject<List<ItemData>>(jsonFile.text);
        foreach (var item in testring)
        {
            Debug.Log($"�̸�: {item.name}, ��������Ʈ �̸�: {item.spriteName}, ����: {item.description}");
            items.Add(new ItemData(item.name, item.place, item.spriteName, item.description));
        }
    }
}
