using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class ItemData
{
    public string name;
    public string spriteName;
    public string description;

    public ItemData(string name, string spriteName, string description)
    {
        this.name = name;
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
        items.Add(new ItemData("��ź", "Bomb_Sprite", "�ֺ����� ���ư� �����մϴ�."));
        items.Add(new ItemData("������", "Sword_Sprite", "��������"));

        string jItems = JsonConvert.SerializeObject(items);
        Debug.Log(jItems);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SaveItem();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LoadItem();
        }
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
        }
    }
}
