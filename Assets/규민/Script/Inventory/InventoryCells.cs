using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCells : MonoBehaviour
{
    [SerializeField] InventoryCell cellPrf;
    [SerializeField] int cell_Count = 36;
    List<InventoryCell> cells = new List<InventoryCell>();

    /// <summary>
    /// 인벤토리 시각화
    /// </summary>
    public void SetInventory()
    {
        int index = 0;
        foreach (var ivData in InventoryManager.Instance.inventoryDatas)
        { 
            cells[index].cellData = ivData;
            cells[index].SetImage();
            index++;
        }

        for (int i = index; i < cells.Count; i++)
        {
            cells[i].cellData = null;
            cells[i].SetImage();
        } 
    }

    private void Start()
    {
        for (int i = 0; i < cell_Count; i++)
        {
            cells.Add(Instantiate(cellPrf, transform));
        }
    }
}
