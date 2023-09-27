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
    public string place;
    public string spriteName;
    public string description;

    public ItemStaticData(string name, string place, string spriteName, string description)
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
        List <ItemStaticData> itemDatas_json = JsonConvert.DeserializeObject<List<ItemStaticData>>(jsonFile.text);
        foreach (var item in itemDatas_json)
        {
            Debug.Log($"이름: {item.name}, 스프라이트 이름: {item.spriteName}, 설명: {item.description}");
            items.Add(new ItemStaticData(item.name, item.place, item.spriteName, item.description));
        }
    }
}
