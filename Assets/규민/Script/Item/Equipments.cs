using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    [Header ("순서-무기,옷,신발,귀고리,반지,펫")]
    public List<InventoryCell> equipCells = new List<InventoryCell>();
    public Dictionary<Enum_GM.ItemPlace, InventoryCell> d_equipments = new Dictionary<Enum_GM.ItemPlace, InventoryCell>();

    /// <summary>
    /// 장착중인 아이템 표시
    /// </summary>
    public void SetEquipments()
    {
        if (!gameObject.activeInHierarchy)
            return;

        foreach (var item in InventoryManager.Instance.d_equipments)
        {
            equipCells[(int)item.Key].cellData = item.Value;
            equipCells[(int)item.Key].SetImage();
        }
    }

    
}
