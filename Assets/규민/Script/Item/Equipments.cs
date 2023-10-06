using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    [Header ("����-����,��,�Ź�,�Ͱ�,����,��")]
    public List<InventoryCell> equipCells = new List<InventoryCell>();
    public Dictionary<Enum_GM.ItemPlace, InventoryCell> d_equipments = new Dictionary<Enum_GM.ItemPlace, InventoryCell>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < equipCells.Count; i++)
            d_equipments.Add((Enum_GM.ItemPlace)i, equipCells[i]);
    }

    /// <summary>
    /// �������� ������ ǥ��
    /// </summary>
    public void SetEquipments()
    {
        if (!gameObject.activeInHierarchy)
            return;

        for (int i = 0; i < equipCells.Count; i++)
            d_equipments[(Enum_GM.ItemPlace)i].SetImage();
    }

    /// <summary>
    /// ��� ����(��ư)
    /// </summary>
    public void OnEquip()
    {
        //����ȭ
        ItemData id = InventoryManager.Instance.SelectedItem;

        //�������� �������� �ִٸ� ����
        if (d_equipments[id.itemStaticData.place].cellData != null)
            OnTakeOff();
        
        //����
        d_equipments[id.itemStaticData.place].cellData = id;
        InventoryManager.Instance.inventoryDatas.Remove(id);
        InventoryManager.Instance.inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// ��� ���� ����(��ư)
    /// </summary>
    public void OnTakeOff()
    {
        //����ȭ
        ItemData id = InventoryManager.Instance.SelectedItem;
        Enum_GM.ItemPlace place = id.itemStaticData.place;

        //���� ����
        InventoryManager.Instance.inventoryDatas.Add(d_equipments[place].cellData);
        d_equipments[place].cellData = null;
        InventoryManager.Instance.inventory_UI.OnCellsEnable();
    }
}
