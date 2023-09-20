using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryItem : MonoBehaviour
{
    #region 고정적
    public Sprite item_Sprite;
    private string itemName;
    private string description;
    #endregion

    #region 변동적
    public Item_Ability[] itemAbility = new Item_Ability[5];
    private string itemRarity;
    #endregion

    public string ItemName { get { return itemName; } set { itemName = value; } }
    public string ItemRarity { get { return itemRarity; } set { itemRarity = value; } }
    public string Description { get { return description; } set { description = value; } }



    protected abstract void Init();

    // Update is called once per frame
    void Update()
    {
        
    }
}
