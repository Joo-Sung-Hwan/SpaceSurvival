using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Newtonsoft.Json;


public struct TestM
{
    public string name;
    public int level;
    public int damage;
    public int speed;
    public TestM(string name, int level,int damage,int speed)
    {
        this.name = name;
        this.level = level;
        this.damage = damage;
        this.speed = speed;
    }
}

public class UpGradeManager : MonoBehaviour
{
    [SerializeField] private List<ItemScriptableData> items;
    [SerializeField] private List<TMP_Text> abilityTexts;

    public Image curItemimage;
    public Image upGradeimage;
    public TMP_Text curIteminfo;
    public TMP_Text upGradeiteminfo;



    public static Color yellow = new Color(1, 1, (74f / 255f));
    public static Color purple = new Color((195f / 255f), 0, 255);
    public static Color blue = new Color((61f / 255f), (167f / 255f), 1);
    public static Color green = Color.green;
    public static Color white = Color.white;

    List<TestM> testM = new List<TestM>();
    string path;

    private void Start()
    {
        testM.Add(new TestM("����", 1, 1, 1));
        testM.Add(new TestM("������", 2, 2, 2));

        path = Application.dataPath + "/";
        foreach (TestM item in testM)
        {
            //SaveData<TestM>("ItemDataNum.Json", item);
        }
        string jitems = JsonConvert.SerializeObject(testM, Formatting.Indented);
        File.WriteAllText(path + "ConvertDataTest.Json", jitems);
    }
    public void SaveData<T>(string fileName, T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path + fileName, jsonData);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetCurItemInfo(RandomItem());
        }
    }
    public void SetCurItemInfo(ItemData item)
    {
        curItemimage.sprite = Resources.Load<Sprite>("ItemIcons/" + item.itemStaticData.spriteName);
        foreach (var itemInfo in item.abilities)
        {
            curIteminfo.text = $"{itemInfo.abilityName} + {itemInfo.abilityValue}%";
        }
    }
    public void SetUpGradeItem()
    {

    }
    public void SetDetails(ItemData item)
    {
        //itemName_Txt.text = item.itemStaticData.name;
        //SetColor(item.rarity, itemName_Txt);

        for (int i = 0; i < abilityTexts.Count; i++)
        {
            if (i + 1 <= item.abilities.Count)
            {
                abilityTexts[i].gameObject.SetActive(true);
                abilityTexts[i].text = AbEnumToString(item.abilities[i].abilityName) + " + " + item.abilities[i].abilityValue + "%";
                SetColor(item.abilities[i].abilityrarity, abilityTexts[i]);
            }
            else
            {
                abilityTexts[i].text = "";
                abilityTexts[i].gameObject.SetActive(false);
            }
        }

        curItemimage.sprite = Resources.Load<Sprite>("ItemIcons/" + item.itemStaticData.spriteName);
    }
    void SetColor(Enum_GM.Rarity rarity, TMP_Text Txt)
    {
        switch (rarity)
        {
            case Enum_GM.Rarity.legendary:
                Txt.color = yellow;
                break;
            case Enum_GM.Rarity.unique:
                Txt.color = purple;
                break;
            case Enum_GM.Rarity.rare:
                Txt.color = blue;
                break;
            case Enum_GM.Rarity.normal:
                Txt.color = white;
                break;
            default:
                break;
        }
    }
    public string AbEnumToString(Enum_GM.abilityName name)
    {
        switch (name)
        {
            case Enum_GM.abilityName.damage:
                return "���ݷ�";
            case Enum_GM.abilityName.range:
                return "���� ����";
            case Enum_GM.abilityName.attackSpeed:
                return "���� �ӵ�";
            case Enum_GM.abilityName.speed:
                return "�̵� �ӵ�";
            default:
                Debug.LogError("���� : itemdetail Ȯ��");
                return "���ݷ�";
        }
    }
    public ItemData RandomItem()
    {
        int rand = Random.Range(0, items.Count);
        ItemScriptableData isd = items[rand];
        ItemStaticData newIsData = new ItemStaticData(isd.ItemName, isd.Place, isd.WeaponKind, isd.SpriteName, isd.Description);

        List<Item_Ability> newItemAbs = new List<Item_Ability>();

        //�������� �����Ƽ�� ����/��ġ�� ������
        //Random.Range(�ּ� ����, �ִ� ����+1)
        for (int i = 0; i < Random.Range(2, 6); i++)
        {
            Item_Ability item_Ab = new Item_Ability();

            int rand_Name = Random.Range(0, 4);
            item_Ab.abilityName = (Enum_GM.abilityName)rand_Name;

            int rand_Rare = Random.Range(0, 100);

            if (rand_Rare < 50)
            {
                item_Ab.abilityrarity = Enum_GM.Rarity.normal;
                item_Ab.abilityValue = 6;
            }
            else if (rand_Rare < 80)
            {
                item_Ab.abilityrarity = Enum_GM.Rarity.rare;
                item_Ab.abilityValue = 10;
            }
            else if (rand_Rare < 95)
            {
                item_Ab.abilityrarity = Enum_GM.Rarity.unique;
                item_Ab.abilityValue = 13;
            }
            else
            {
                item_Ab.abilityrarity = Enum_GM.Rarity.legendary;
                item_Ab.abilityValue = 15;
            }

            item_Ab.abilityValue += Random.Range(0, 4);
            newItemAbs.Add(item_Ab);
            //�μ��� ����ִ� ���ٽĿ� ���� ����Ʈ�� ����
            //-> abilityValue���� ũ�� ������ ����
            newItemAbs.Sort((Item_Ability ab_A, Item_Ability ab_B) => ab_B.abilityValue.CompareTo(ab_A.abilityValue));
        }
        //������ ��ü�� ��͵� (���� ���� ����� ability ��͵��� ����)
        Enum_GM.Rarity newRarity = (Enum_GM.Rarity)3;
        foreach (var item in newItemAbs)
        {
            if (item.abilityrarity < newRarity)
                newRarity = item.abilityrarity;
        }

        return new ItemData(newIsData, newRarity, newItemAbs);
    }
}
