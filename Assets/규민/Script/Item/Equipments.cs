using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    [Header ("����-����,��,�Ź�,�Ͱ�,����,��")]
    public List<InventoryCell> equipCells = new List<InventoryCell>();
    public Dictionary<Enum_GM.ItemPlace, InventoryCell> d_equipments = new Dictionary<Enum_GM.ItemPlace, InventoryCell>();

    /// <summary>
    /// �������� ������ ǥ��
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
