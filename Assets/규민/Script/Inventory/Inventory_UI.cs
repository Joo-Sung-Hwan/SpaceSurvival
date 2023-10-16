using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public InventoryCells cells;
    
    /// <summary>
    /// ������ ǥ���ϴ� �Լ� (Inventory_Animation Event)
    /// </summary>
    public void OnCellsEnable()
    {
        cells.SetInventory();
        InventoryManager.Instance.SetEquipments();
        InventoryManager.Instance.SaveInventory();
        InventoryManager.Instance.SaveEquipment();
    }
}
