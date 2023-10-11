using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public InventoryCells cells;
    public Equipments equipments;

    public void OnCellsEnable()
    {
        cells.SetInventory();
        equipments.SetEquipments();
        InventoryManager.Instance.SaveInventory();
        InventoryManager.Instance.SaveEquipment();
    }
}
