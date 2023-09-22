using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [HideInInspector] public InventoryData cellData;
    public Image itemImage;
    [SerializeField] GameObject itemButton;

    public void SetImage()
    {
        itemButton.SetActive(cellData != null);
    }
}
