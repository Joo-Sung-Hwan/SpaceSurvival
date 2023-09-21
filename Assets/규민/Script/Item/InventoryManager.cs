using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public ItemData itemData;
    public string rarity;
    public List<Item_Ability> abilities;

    public InventoryData(ItemData itemData)
    {
        this.itemData = itemData;
    }
}

public class InventoryManager : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddItem()
    {

    }
}
