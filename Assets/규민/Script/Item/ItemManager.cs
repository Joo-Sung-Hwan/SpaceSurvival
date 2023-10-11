using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

/// <summary>
/// Item의 변하지 않는 데이터(이름, 장착부위 등등)
/// </summary>
[System.Serializable]
public class ItemStaticData
{
    public string name;
    public Enum_GM.ItemPlace place;
    public string spriteName;
    public string description;

    public ItemStaticData(string name, Enum_GM.ItemPlace place, string spriteName, string description)
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

    [HideInInspector] public List<ItemStaticData> items = new List<ItemStaticData>();

    // Start is called before the first frame update
    void Start()
    {
        LoadItem();
    }

    private void Update()
    {

    }

    public void SaveItem()
    {
        string jitems = JsonConvert.SerializeObject(items , Formatting.Indented);
        //File.WriteAllText(Application.dataPath + "/Items.json" , jitems);
        File.WriteAllText(jsonFile.text, jitems);
    }

    public void LoadItem()
    { 
        //string jitem = File.ReadAllText(Application.dataPath + "/Items.json");
        List <ItemStaticData> itemDatas_json = JsonConvert.DeserializeObject<List<ItemStaticData>>(jsonFile.text);
        foreach (var item in itemDatas_json)
        {
            items.Add(new ItemStaticData(item.name, item.place, item.spriteName, item.description));
        }
    }
}
