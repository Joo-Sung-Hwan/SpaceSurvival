using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_AdMob : ShopManager
{
    [SerializeField] private ItemDetail itemDetail;
    protected override void OnClickBuyButtonDown()
    {
        base.OnClickBuyButtonDown();
        itemDetail.gameObject.SetActive(true);
        ItemData newItem = RandomItem();
        itemDetail.SetDetails(newItem);
        InventoryManager.Instance.AddItem(newItem);
    }
}
