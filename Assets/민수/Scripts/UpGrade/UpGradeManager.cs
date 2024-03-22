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
    [SerializeField] private List<TMP_Text> cur_AbilityTexts;
    [SerializeField] private List<TMP_Text> up_AbilityTexts;
    [SerializeField] private TMP_Text goldNcost;
    [SerializeField] private UpGradePopUp popUp;
    [SerializeField] private Image failImage;
    [SerializeField] private Image curItemimage;
    [SerializeField] private Image upGradeimage;
    [SerializeField] private TMP_Text curItemName;
    [SerializeField] private TMP_Text upGradeItemName;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button failButton;

    public static Color yellow = new Color(1, 1, (74f / 255f));
    public static Color purple = new Color((195f / 255f), 0, 255);
    public static Color blue = new Color((61f / 255f), (167f / 255f), 1);
    public static Color green = Color.green;
    public static Color white = Color.white;

    [HideInInspector] public bool isOn = false;
    private ItemData item;
    private int cost;
    private void Awake()
    {
        okButton.onClick.AddListener(() => OnOKButtonDown());
        cancelButton.onClick.AddListener(() => OnCancelButtonDown());
        failButton.onClick.AddListener(() => OnFailButtonDown());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetCurItemInfo(RandomItem());
        }
        
    }
    public void OnOKButtonDown()
    {
        if (isOn == true) return;

        if (InventoryManager.Instance.Gold >= cost)
        {
            popUp.gameObject.SetActive(true);
            popUp.SetAction(UpGradedItemDataToInventory,this);
        }
        else
        {
            failImage.gameObject.SetActive(true);
            popUp.SetAction(UpGradedItemDataToInventory, this);
        }
        isOn = true;
    }
    public void OnCancelButtonDown()
    {
        if (isOn == true) return;

        InventoryManager.Instance.IsUpGrade = false;
        item = null;
        gameObject.SetActive(false);
    }
    public void OnFailButtonDown()
    {
        failImage.gameObject.SetActive(false);
    }
    private void UpGradedItemDataToInventory()
    {
        InventoryManager inven = InventoryManager.Instance;
        inven.inventoryDatas.Remove(InventoryManager.Instance.SelectedItem);
        inven.inventoryDatas.Add(item);
        inven.inventory_UI.OnCellsEnable();
        inven.IsUpGrade = false;
        inven.Gold -= cost;
        item = null;
        gameObject.SetActive(false);
    }

    public void SetCurItemInfo(ItemData item)
    {
        curItemimage.sprite = Resources.Load<Sprite>("ItemIcons/" + item.itemStaticData.spriteName);

        SetCurItem(item,cur_AbilityTexts);
        SetUpGradeItem(item,up_AbilityTexts);
    }
    public void SetUpGradeItem(ItemData item,List<TMP_Text> texts)
    {
        ItemData upItem = SetUpGradeItemData(item);
        upGradeimage.sprite = curItemimage.sprite;
        upGradeItemName.text = $"{upItem.itemStaticData.name}+{upItem.level}";
        SetColor(upItem.rarity, upGradeItemName);

        for (int i = 0; i < texts.Count; i++)
        {
            if (i + 1 <= upItem.abilities.Count)
            {
                texts[i].gameObject.SetActive(true);
                texts[i].text = AbEnumToString(upItem.abilities[i].abilityName) + " + " + upItem.abilities[i].abilityValue + "%";
                SetColor(upItem.abilities[i].abilityrarity, texts[i]);
            }
            else
            {
                texts[i].text = "";
                texts[i].gameObject.SetActive(false);
            }
        }
        SetColor(upItem.rarity, upGradeItemName);
        SetCost(goldNcost, upItem);
        this.item = upItem;
    }
    public void SetCurItem(ItemData item,List<TMP_Text> texts)
    {
        this.item = item;

        //curItemName.text = item.itemStaticData.name;
        curItemName.text = $"{item.itemStaticData.name}+{item.level}";

        for (int i = 0; i < texts.Count; i++)
        {
            if (i + 1 <= item.abilities.Count)
            {
                texts[i].gameObject.SetActive(true);
                texts[i].text = AbEnumToString(item.abilities[i].abilityName) + " + " + item.abilities[i].abilityValue + "%";
                SetColor(item.abilities[i].abilityrarity, texts[i]);
            }
            else
            {
                texts[i].text = "";
                texts[i].gameObject.SetActive(false);
            }
        }
        SetColor(item.rarity, curItemName);
        curItemimage.sprite = Resources.Load<Sprite>("ItemIcons/" + item.itemStaticData.spriteName);
        SetCost(goldNcost, item);
    }
    private void SetCost(TMP_Text text, ItemData item)
    {
        int gold = InventoryManager.Instance.Gold;
        int basicCost = 1000;
        if (item.level > 0)
        {
            basicCost *= item.level;
        }

        if ((int)item.rarity != 0)
        {
            cost = basicCost / (int)item.rarity;
        }

        if (gold >= cost)
        {
            text.color = Color.green;
        }
        else
        {
            text.color = Color.red;
        }
        cost = basicCost;
        text.text = $"{gold}/{cost}";
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
                return "공격력";
            case Enum_GM.abilityName.range:
                return "공격 범위";
            case Enum_GM.abilityName.attackSpeed:
                return "공격 속도";
            case Enum_GM.abilityName.speed:
                return "이동 속도";
            default:
                Debug.LogError("오류 : itemdetail 확인");
                return "공격력";
        }
    }

    private ItemData SetUpGradeItemData(ItemData item)
    {
        List<Item_Ability> newItemAbs = new List<Item_Ability>();
        
        ItemData newItemData = item;
        newItemData.level += 1;

        for (int i = 0; i < item.abilities.Count; i++)
        {
            Item_Ability item_Ab = new Item_Ability();

            item_Ab.abilityName = item.abilities[i].abilityName;
            item_Ab.abilityValue = item.abilities[i].abilityValue + 1;
            
            if (item_Ab.abilityValue > 0 && item_Ab.abilityValue <= 6)
            {
                item_Ab.abilityrarity = Enum_GM.Rarity.normal;
            }
            else if (item_Ab.abilityValue <= 10)
            {
                item_Ab.abilityrarity = Enum_GM.Rarity.rare;
            }
            else if (item_Ab.abilityValue <= 13)
            {
                item_Ab.abilityrarity = Enum_GM.Rarity.unique;
            }
            else
            {
                item_Ab.abilityrarity = Enum_GM.Rarity.legendary;
            }
            newItemAbs.Add(item_Ab);
        }
        if (newItemAbs[0].abilityValue >= 0)
        {
            newItemData.rarity = Enum_GM.Rarity.normal;
        }
        else if (newItemAbs[0].abilityValue >= 10)
        {
            newItemData.rarity = Enum_GM.Rarity.rare;
        }
        else if (newItemAbs[0].abilityValue >= 20)
        {
            newItemData.rarity = Enum_GM.Rarity.unique;
        }
        else
        {
            newItemData.rarity = Enum_GM.Rarity.legendary;
        }

        return newItemData;
    }

    public ItemData RandomItem()
    {
        int rand = Random.Range(0, items.Count);
        ItemScriptableData isd = items[rand];
        ItemStaticData newIsData = new ItemStaticData(isd.ItemName, isd.Place, isd.WeaponKind, isd.SpriteName, isd.Description);

        List<Item_Ability> newItemAbs = new List<Item_Ability>();

        //랜덤으로 어빌리티의 종류/수치를 결정함
        //Random.Range(최소 개수, 최대 개수+1)
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
            //인수에 들어있는 람다식에 따라 리스트를 정렬
            //-> abilityValue값이 크면 앞으로 정렬
            newItemAbs.Sort((Item_Ability ab_A, Item_Ability ab_B) => ab_B.abilityValue.CompareTo(ab_A.abilityValue));
        }
        //아이템 자체의 희귀도 (가장 높은 등급의 ability 희귀도를 따라감)
        Enum_GM.Rarity newRarity = (Enum_GM.Rarity)3;

        foreach (var item in newItemAbs)
        {
            if (item.abilityrarity < newRarity)
                newRarity = item.abilityrarity;
        }
        return new ItemData(newIsData, newRarity, newItemAbs,0);
    }
}
