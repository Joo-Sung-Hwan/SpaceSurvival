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


    [Header("장비칸 (순서-무기,옷,신발,귀고리,반지,펫)")]
    public List<InventoryCell> equipCells = new List<InventoryCell>();

    #region 정렬 - 선언
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

    #region 싱글톤(Awake 포함)
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
    /// 아이템 추가 함수(테스트)
    /// </summary>
    /// <param name="itemDatas"></param>
    void AddItem(List<ItemData> itemDatas)
    {
        int rand = Random.Range(0, itemManager.items.Count);
        ItemStaticData newIsData = itemManager.items[rand];

        //아이템 자체의 희귀도

        //임시
        int randRarity = Random.Range(1, 4);

        Enum_GM.Rarity newRarity = (Enum_GM.Rarity)randRarity;

        List<Item_Ability> newItemAbs = new List<Item_Ability>();

        //랜덤으로 어빌리티의 종류/수치를 결정함
        // i = 최대 ability 개수
        for (int i = 0; i < 5; i++)
        {
            Item_Ability item_Ab = new Item_Ability();

            int rand_Name = Random.Range(0, 100);
            item_Ab.abilityName = rand_Name < 50 ? "공격력" : "공격 범위";

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
    /// 장비 장착(버튼)
    /// </summary>
    public void OnEquip()
    {
        //장착중인 아이템이 있다면 해제
        if (d_equipments.ContainsKey(SelectedItem.itemStaticData.place))
            OnTakeOff();

        //장착
        //d_equipments[SelectedItem.itemStaticData.place] = SelectedItem;
        d_equipments.Add(SelectedItem.itemStaticData.place, SelectedItem);
        PutCellData(SelectedItem.itemStaticData.place);
        inventoryDatas.Remove(SelectedItem);
        inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// 장비 장착 해제(버튼)
    /// </summary>
    public void OnTakeOff()
    {
        //간략화
        Enum_GM.ItemPlace place = SelectedItem.itemStaticData.place;

        //장착 해제
        inventoryDatas.Add(d_equipments[place]);
        d_equipments.Remove(place);
        PutCellData(place);
        inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// 아이템 삭제 함수(ItemDetail - ItemDestroy 버튼)
    /// </summary>
    public void OnRemoveItem()
    {
        inventoryDatas.Remove(selectedItem);
        inventory_UI?.OnCellsEnable();
    }

    /// <summary>
    /// 장비창 새로고침
    /// </summary>
    public void SetEquipments()
    {
        foreach (var item in equipCells)
            item.SetImage();
    }

    /// <summary>
    /// 장비 셀에 장비 정보를 넣어주는 함수
    /// </summary>
    void PutCellData(Enum_GM.ItemPlace place)
    {
        equipCells[(int)place].cellData = d_equipments.ContainsKey(place) ? d_equipments[place] : null;
    }
    #region 정렬
    /// <summary>
    /// 정렬 기준을 설정하는 함수 - Sort_Dropdown의 OnValueChange
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
    /// 정렬 기준(sortBy)에 따라 정렬 함수를 호출하는 함수 - Sort_Button의 OnClick
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
    /// 이름순으로 정렬하는 함수
    /// </summary>
    void SortItemByName()
    {
        inventoryDatas.Sort((ItemData id_A , ItemData id_B) => id_A.itemStaticData.name.CompareTo(id_B.itemStaticData.name));
        inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// 레어도순으로 정렬하는 함수
    /// </summary>
    void SortItemByRare()
    {
        inventoryDatas.Sort((ItemData id_A, ItemData id_B) => id_A.rarity.CompareTo(id_B.rarity));
        inventory_UI.OnCellsEnable();
    } 
    #endregion
}
