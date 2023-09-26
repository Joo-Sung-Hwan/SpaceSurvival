using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public ItemData itemData;
    public Enum_GM.Rarity rarity;
    public List<Item_Ability> abilities;

    public InventoryData(ItemData itemData, Enum_GM.Rarity rarity, List<Item_Ability> abilities)
    {
        this.itemData = itemData;
        this.rarity = rarity;
        this.abilities = abilities;
    }
}

public class InventoryManager : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    public ItemDetail itemDetail;
    [HideInInspector] public List<InventoryData> inventoryDatas = new List<InventoryData>();

    #region �̱���
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

        //������ ��ü�� ��͵�
        Enum_GM.Rarity newRarity = Enum_GM.Rarity.normal;

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


        inventoryDatas.Add(new InventoryData(newItemData, newRarity, newItemAbs));
        Debug.Log($"������ �߰� : {newItemData.name}");
    }


}
