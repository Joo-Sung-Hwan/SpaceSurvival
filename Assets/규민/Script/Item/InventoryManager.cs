using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [HideInInspector] public Dictionary<Enum_GM.ItemPlace , ItemData> equipDatas= new Dictionary<Enum_GM.ItemPlace, ItemData>();

    #region 정렬
    [SerializeField] UnityEngine.UI.Dropdown dropdown;

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

    #region 싱글톤
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddItem(inventoryDatas);
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
        Enum_GM.Rarity newRarity = Enum_GM.Rarity.rare;

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
        Debug.Log($"아이템 추가 : {newIsData.name}");
    }

    void SortItemByName()
    {
        inventoryDatas.Sort((ItemData id_A , ItemData id_B) => id_A.itemStaticData.name.CompareTo(id_B.itemStaticData.name));
        inventory_UI.OnCellsEnable();
    }

    void SortItemByRare()
    {
        inventoryDatas.Sort((ItemData id_A, ItemData id_B) => id_A.rarity.CompareTo(id_B.rarity));
        inventory_UI.OnCellsEnable();
    }

    public void OnSortBy()
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
}
