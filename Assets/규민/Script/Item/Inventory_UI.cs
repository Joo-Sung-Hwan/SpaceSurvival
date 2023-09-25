using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public InventoryCells cells;

    public void OnCellsEnable()
    {
        cells.SetInventory();
    }
}
