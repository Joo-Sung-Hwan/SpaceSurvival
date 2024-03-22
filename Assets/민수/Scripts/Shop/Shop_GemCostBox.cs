using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_GemCostBox : ShopManager
{
    [SerializeField] private ItemDetail itemDetail;

    private int gemCost = 1000;
    protected override void OnClickBuyButtonDown()
    {
        base.OnClickBuyButtonDown();

        if (InventoryManager.Instance.Gem >= gemCost)
        {
            // 잼이 충분할때
            buycheck.gameObject.SetActive(true);
            buycheck.SetBuyCheckData(ItemDataToCheckObect, product_Image.sprite);
        }
        else
        {
            // 잼이 부족할때
            failImage.SetActive(true);
        }
    }
    void ItemDataToCheckObect()
    {
        itemDetail.gameObject.SetActive(true);
        ItemData newItem = RandomItem();
        itemDetail.SetDetails(newItem);
        InventoryManager.Instance.AddItem(newItem);
    }
}
