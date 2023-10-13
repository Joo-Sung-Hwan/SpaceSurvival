using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public InventoryCells cells;
    public Equipments equipments;
    
    /// <summary>
    /// ������ ǥ���ϴ� �Լ�
    /// </summary>
    public void OnCellsEnable()
    {
        cells.SetInventory();
        InventoryManager.Instance.SetEquipments();
        InventoryManager.Instance.SaveInventory();
        InventoryManager.Instance.SaveEquipment();
    }
}
