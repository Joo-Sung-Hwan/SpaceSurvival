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
            // ���� ����Ҷ�
            buycheck.gameObject.SetActive(true);
            buycheck.SetBuyCheckData(ItemDataToCheckObect, product_Image.sprite);
        }
        else
        {
            // ���� �����Ҷ�
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
