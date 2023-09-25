using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public ItemData itemData;
    public string rarity;
    public List<Item_Ability> abilities;

    public InventoryData(ItemData itemData, string rarity, List<Item_Ability> abilities)
    {
        this.itemData = itemData;
        this.rarity = rarity;
        this.abilities = abilities;
    }
}

public class InventoryManager : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    [HideInInspector] public List<InventoryData> inventoryDatas = new List<InventoryData>();

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
            AddItem();
        }
    }

    void AddItem()
    {
        int rand = Random.Range(0, itemManager.items.Count);
        ItemData newItemData = itemManager.items[rand];

        string newRarity = "노말";

        List<Item_Ability> newItemAbs = new List<Item_Ability>();

        // i = 최대 ability 개수
        for (int i = 0; i < 5; i++)
        {
            Item_Ability item_Ab = new Item_Ability();

            int rand_Name = Random.Range(0, 100);
            item_Ab.abilityName = rand_Name < 50 ? "공격력" : "공격 범위";

            int rand_Rare = Random.Range(0, 100);

            if (rand_Rare < 50)
            {
                item_Ab.abilityrarity = "노말";
                item_Ab.abilityValue = 6;
            }
            else if (rand_Rare < 80)
            {
                item_Ab.abilityrarity = "레어";
                item_Ab.abilityValue = 10;
            }
            else if (rand_Rare < 95)
            {
                item_Ab.abilityrarity = "유니크";
                item_Ab.abilityValue = 13;
            }
            else
            {
                item_Ab.abilityrarity = "레전더리";
                item_Ab.abilityValue = 15;
            }

            item_Ab.abilityValue += Random.Range(0, 4);
        }


        inventoryDatas.Add(new InventoryData(newItemData, newRarity, newItemAbs));
        Debug.Log($"아이템 추가 : {newItemData.name}");
    }
}
