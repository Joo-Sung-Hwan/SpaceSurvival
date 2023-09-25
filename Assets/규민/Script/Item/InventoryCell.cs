using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    /*[HideInInspector]*/ public InventoryData cellData = null;
    public Image itemImage;
    [SerializeField] GameObject itemButton;

    public void SetImage()
    {
        itemButton.SetActive(cellData.rarity != "");
        itemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + cellData.itemData.spriteName);
    }
}
