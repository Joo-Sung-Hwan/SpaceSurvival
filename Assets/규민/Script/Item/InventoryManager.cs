using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

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

public class InventoryManager : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    public Inventory_UI inventory_UI;
    public ItemDetail itemDetail;
    public ItemDetail equipmentDetail;
    [HideInInspector] public List<ItemData> inventoryDatas = new List<ItemData>();
    [HideInInspector] public Dictionary<Enum_GM.ItemPlace , ItemData> d_equipments= new Dictionary<Enum_GM.ItemPlace, ItemData>();


    [Header("���ĭ (����-����,��,�Ź�,�Ͱ�,����,��)")]
    public List<InventoryCell> equipCells = new List<InventoryCell>();

    #region ���� - ����
    [SerializeField] TMPro.TMP_Dropdown dropdown;

    Enum_GM.SortBy sortBy = Enum_GM.SortBy.name;
    #endregion

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
            DontDestroyOnLoad(gameObject);
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddItem(inventoryDatas);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log(equipCells[0].cellData.itemStaticData.name);
        }
    }

    /// <summary>
    /// ������ �߰� �Լ�(�׽�Ʈ)
    /// </summary>
    /// <param name="itemDatas"></param>
    void AddItem(List<ItemData> itemDatas)
    {
        int rand = Random.Range(0, itemManager.items.Count);
        ItemStaticData newIsData = itemManager.items[rand];

        //������ ��ü�� ��͵�

        //�ӽ�
        int randRarity = Random.Range(1, 4);

        Enum_GM.Rarity newRarity = (Enum_GM.Rarity)randRarity;

        List<Item_Ability> newItemAbs = new List<Item_Ability>();

        //�������� �����Ƽ�� ����/��ġ�� ������
        // i = �ִ� ability ����
        for (int i = 0; i < 5; i++)
        {
            Item_Ability item_Ab = new Item_Ability();

            int rand_Name = Random.Range(0, 100);
            item_Ab.abilityName = rand_Name < 50 ? "���ݷ�" : "���� ����";

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


        itemDatas.Add(new ItemData(newIsData, newRarity, newItemAbs));
        inventory_UI?.OnCellsEnable();
    }

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

    /// <summary>
    /// ��� ����(��ư)
    /// </summary>
    public void OnEquip()
    {
        //�������� �������� �ִٸ� ����
        if (d_equipments.ContainsKey(SelectedItem.itemStaticData.place))
            OnTakeOff();

        //����
        //d_equipments[SelectedItem.itemStaticData.place] = SelectedItem;
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
    }

    /// <summary>
    /// ��� ���� ��� ������ �־��ִ� �Լ�
    /// </summary>
    void PutCellData(Enum_GM.ItemPlace place)
    {
        equipCells[(int)place].cellData = d_equipments.ContainsKey(place) ? d_equipments[place] : null;
    }
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
