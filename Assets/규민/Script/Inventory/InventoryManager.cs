using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;

#region ������ class, struct
[System.Serializable]
public class ItemData
{
    public ItemStaticData itemStaticData;
    public Enum_GM.Rarity rarity;
    public List<Item_Ability> abilities;

    public ItemData(ItemStaticData itemStaticData, Enum_GM.Rarity rarity, List<Item_Ability> abilities)
    {
        this.itemStaticData = itemStaticData;
        this.rarity = rarity;
        this.abilities = abilities;
    }
}

[System.Serializable]
//������ ������ Ư¡ (�̸��̳� ���� �������� ������ �ٸ� ��쿡�� �޶����� Ư¡)
public class ItemStaticData
{
    public string name;
    public Enum_GM.ItemPlace place;
    public PlayerWeapon weaponKind;
    public string spriteName;
    public string description;
    public int itemLevel;

    public ItemStaticData(string name, Enum_GM.ItemPlace place, PlayerWeapon weaponKind, string spriteName, string description,int itemLevel)
    {
        this.name = name;
        this.place = place;
        this.weaponKind = weaponKind;
        this.spriteName = spriteName;
        this.description = description;
        this.itemLevel = itemLevel;
    }
}

//������ �ɷ�ġ
public struct Item_Ability
{
    public Enum_GM.abilityName abilityName;
    public float abilityValue;
    public Enum_GM.Rarity abilityrarity;
}
#endregion

public class InventoryManager : MonoBehaviour
{
    #region Hierarchy���� �־��־�� �� �͵�
    [Header("ItemDatas(ScriptableObj)")]
    [SerializeField] List<ItemScriptableData> isdList;

    [SerializeField] TMP_Text totalAb_Text;
    [SerializeField] TMP_Dropdown dropdown;
    public Inventory_UI inventory_UI;
    public ItemDetail itemDetail;
    public ItemDetail equipmentDetail;

    [Header("���ĭ (����-����,��,�Ź�,�Ͱ�,����,��)")]
    public List<InventoryCell> equipCells = new List<InventoryCell>();

    [Header("���")]
    [SerializeField] TMP_Text lobbyGold_Text;
    [SerializeField] TMP_Text shopGold_Text;
    #endregion

    //�κ��丮�� ����ִ� �����۵� ����Ʈ
    [HideInInspector] public List<ItemData> inventoryDatas = new List<ItemData>();
    //������ ���� ������
    [HideInInspector] public Dictionary<Enum_GM.ItemPlace, ItemData> d_equipments = new Dictionary<Enum_GM.ItemPlace, ItemData>();
    //�ɷº� �ɷ�ġ ������
    [HideInInspector] public Dictionary<Enum_GM.abilityName, float> d_totalAb = new Dictionary<Enum_GM.abilityName, float>();
    //���ø������ ��Ÿ���� ����
    [HideInInspector] public bool isSelectMode = false;
    ///���ø�忡�� ������ �����۵� ����Ʈ
    [HideInInspector] public List<InventoryCell> selectedCells = new List<InventoryCell>();

    public bool IsUpGrade { get; set; }
    //���
    private int gold = 300000;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            lobbyGold_Text.text = value.ToString();
            shopGold_Text.text = value.ToString();
            gold = value;
        }
    }
    
    //���� ����
    Enum_GM.SortBy sortBy = Enum_GM.SortBy.name;

    //������ ������(���ø��x)
    public ItemData SelectedItem 
    { 
        get 
        { 
            return selectedItem; 
        } 
        set 
        {
            itemDetail.SetDetails(value);
            equipmentDetail.SetDetails(value);
            selectedItem = value;
        } 
    }
    private ItemData selectedItem;

    #region �̱���(Awake ����)
    public static InventoryManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public static InventoryManager Instance
    {
        get { return instance; }
    }
    #endregion

    private void Start()
    {
        LoadInventory();
        LoadEquipment();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddItem(RandomItem());
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Gold += 500;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 0; i < InventoryManager.Instance.inventoryDatas.Count; i++)
            {
                for (int j = 0; j < InventoryManager.Instance.inventoryDatas[i].abilities.Count; j++)
                {
                    Debug.Log($"{InventoryManager.Instance.inventoryDatas[i].abilities[j].abilityName}:{InventoryManager.Instance.inventoryDatas[i].abilities[j].abilityValue}");
                }
            }
        }
    }

    #region ������ �߰�
    /// <summary>
    /// ���� ������ ��ȯ �Լ�
    /// </summary>
    /// <param name="itemDatas"></param>
    public ItemData RandomItem()
    {
        int rand = Random.Range(0, isdList.Count);
        ItemScriptableData isd = isdList[rand];
        ItemStaticData newIsData = new ItemStaticData(isd.ItemName, isd.Place, isd.WeaponKind, isd.SpriteName, isd.Description,0);

        List<Item_Ability> newItemAbs = new List<Item_Ability>();

        //�������� �����Ƽ�� ����/��ġ�� ������
        //Random.Range(�ּ� ����, �ִ� ����+1)
        for (int i = 0; i < Random.Range(2,6); i++)
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

    public void AddItem(ItemData id)
    {
        inventoryDatas.Add(id);
        inventory_UI?.OnCellsEnable();
    }
    #endregion

    #region ������ ������ ����/�ҷ�����
    public void SaveInventory()
    {
        string jitems = JsonConvert.SerializeObject(inventoryDatas, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Inventory.json", jitems);
    }

    void LoadInventory()
    {
        string str = File.ReadAllText(Application.dataPath + "/Inventory.json");
        List<ItemData> itemDatas_json = JsonConvert.DeserializeObject<List<ItemData>>(str);
        foreach (var item in itemDatas_json)
        {
            inventoryDatas.Add(new ItemData(item.itemStaticData , item.rarity, item.abilities));
        }
    }

    public void SaveEquipment()
    {
        string jEquipments = JsonConvert.SerializeObject(d_equipments, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Equipments.json", jEquipments);
    }

    private void LoadEquipment()
    {
        string str = File.ReadAllText(Application.dataPath + "/Equipments.json");
        Dictionary<Enum_GM.ItemPlace, ItemData> equipmentDatas_json = JsonConvert.DeserializeObject<Dictionary<Enum_GM.ItemPlace, ItemData>>(str);
        foreach (var item in equipmentDatas_json)
        {
            d_equipments.Add(item.Key ,(new ItemData(item.Value.itemStaticData, item.Value.rarity, item.Value.abilities)));
            PutCellData(item.Key);
        }
    }
    #endregion

    #region ��� ����/����
    /// <summary>
    /// ��� ����(��ư)
    /// </summary>
    public void OnEquip()
    {
        //�������� �������� �ִٸ� ����
        if (d_equipments.ContainsKey(SelectedItem.itemStaticData.place))
            OnTakeOff();

        //����
        d_equipments.Add(SelectedItem.itemStaticData.place, SelectedItem);
        PutCellData(SelectedItem.itemStaticData.place);
        inventoryDatas.Remove(SelectedItem);
        inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// ��� ���� ����(��ư)
    /// </summary>
    public void OnTakeOff()
    {
        //����ȭ
        Enum_GM.ItemPlace place = SelectedItem.itemStaticData.place;

        //���� ����
        inventoryDatas.Add(d_equipments[place]);
        d_equipments.Remove(place);
        PutCellData(place);
        inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// ������ ���� �Լ�(ItemDetail - ItemDestroy ��ư)
    /// </summary>
    public void OnRemoveItem()
    {
        inventoryDatas.Remove(selectedItem);
        inventory_UI?.OnCellsEnable();
    }

    /// <summary>
    /// ���â ���ΰ�ħ
    /// </summary>
    public void SetEquipments()
    {
        foreach (var item in equipCells)
            item.SetImage();

        SetTotalAbTxt();
    }

    /// <summary>
    /// �� �ɷ�ġ ������ ǥ��
    /// </summary>
    public void SetTotalAbTxt()
    {
        totalAb_Text.text = "";
        foreach (var item in d_totalAb)
        {
            totalAb_Text.text += $"{itemDetail.AbEnumToString(item.Key)} + {item.Value}%" + "\n";
        }
    }

    /// <summary>
    /// ��� ���� ��� ������ �־��ִ� �Լ�
    /// </summary>
    void PutCellData(Enum_GM.ItemPlace place)
    {
        //��� ��ųʸ��� �ִ� ��� ������ ���â(��)�� ����ȭ
        if (d_equipments.ContainsKey(place))
        {
            equipCells[(int)place].cellData = d_equipments[place];

            //�����Ƽ ���հ� ���
            foreach (var item in equipCells[(int)place].cellData.abilities)
            {
                if (!d_totalAb.TryAdd(item.abilityName, item.abilityValue))
                {
                    d_totalAb[item.abilityName] += item.abilityValue;
                }
            }
        }
        else
        {
            //�����Ƽ ���հ� ���
            foreach (var item in equipCells[(int)place].cellData.abilities)
            {
                d_totalAb[item.abilityName] -= item.abilityValue;
                if (d_totalAb[item.abilityName] <= 0)
                    d_totalAb.Remove(item.abilityName);
            }

            equipCells[(int)place].cellData = null;
        }
    }
    #endregion

    #region ���� ���
    /// <summary>
    /// ���ø�忡�� ������ ������ �ϰ� ����
    /// </summary>
    public void OnRemoveSeletedItem()
    {
        foreach (var item in selectedCells)
        {
            inventoryDatas.Remove(item.cellData);
            item.IsSelected = false;
        }

        selectedCells.Clear();
        inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// ���õ� ������ ����Ʈ ����
    /// </summary>
    public void SelectModeOff()
    {
        foreach (var item in selectedCells)
            item.IsSelected = false;

        selectedCells.Clear();
    }
    #endregion

    #region ����
    /// <summary>
    /// ���� ������ �����ϴ� �Լ� - Sort_Dropdown�� OnValueChange
    /// </summary>
    public void OnSetSortBy()
    {
        switch (dropdown.value)
        {
            case 0:
                sortBy = Enum_GM.SortBy.name;
                break;
            case 1:
                sortBy = Enum_GM.SortBy.rare;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ���� ����(sortBy)�� ���� ���� �Լ��� ȣ���ϴ� �Լ� - Sort_Button�� OnClick
    /// </summary>
    public void OnSort()
    {
        switch (sortBy)
        {
            case Enum_GM.SortBy.name:
                SortItemByName();
                break;
            case Enum_GM.SortBy.rare:
                SortItemByRare();
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// �̸������� �����ϴ� �Լ�
    /// </summary>
    void SortItemByName()
    {
        inventoryDatas.Sort((ItemData id_A , ItemData id_B) => id_A.itemStaticData.name.CompareTo(id_B.itemStaticData.name));
        inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// ��������� �����ϴ� �Լ�
    /// </summary>
    void SortItemByRare()
    {
        inventoryDatas.Sort((ItemData id_A, ItemData id_B) => id_A.rarity.CompareTo(id_B.rarity));
        inventory_UI.OnCellsEnable();
    } 
    #endregion
}
