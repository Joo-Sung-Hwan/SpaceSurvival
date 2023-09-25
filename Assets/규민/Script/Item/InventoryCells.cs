using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCells : MonoBehaviour
{
    [SerializeField] List<InventoryCell> cells;

    public void SetInventory()
    {
        int index = 0;
        foreach (var ivData in InventoryManager.Instance.inventoryDatas)
        { 
            cells[index].cellData = ivData;
            index++;
        }

        foreach (var cell in cells)
        {
            cell.SetImage();
        }
    }
}
