using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCells : MonoBehaviour
{
    [SerializeField] List<InventoryCell> cells;

    public void SetInventory()
    {
        foreach (var ivData in InventoryManager.Instance.inventoryDatas)
        {
            int index = 0;
            cells[index].cellData = ivData;
            index++;
        }

        foreach (var cell in cells)
        {
            cell.SetImage();
        }
    }

    private void OnEnable()
    {
        SetInventory();
    }
}
