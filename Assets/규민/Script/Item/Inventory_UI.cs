using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public InventoryCells cells;
    public Equipments equipments;
    
    /// <summary>
    /// 아이템 표시하는 함수
    /// </summary>
    public void OnCellsEnable()
    {
        cells.SetInventory();
        InventoryManager.Instance.SetEquipments();
        InventoryManager.Instance.SaveInventory();
        InventoryManager.Instance.SaveEquipment();
    }
}
