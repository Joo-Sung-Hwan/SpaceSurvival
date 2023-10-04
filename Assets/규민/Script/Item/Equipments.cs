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

    public void SetEquipments()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        for (int i = 0; i < equipCells.Count; i++)
        {
            d_equipments[(Enum_GM.ItemPlace)i].SetImage();
        }
    }

    public void OnEquip()
    {
        ItemData id = InventoryManager.Instance.SelectedItem;
        d_equipments[StringToItemPlace(id.itemStaticData.place)].cellData = id;
        InventoryManager.Instance.inventoryDatas.Remove(id);
        InventoryManager.Instance.inventory_UI.OnCellsEnable();
    }

    Enum_GM.ItemPlace StringToItemPlace(string str)
    {
        switch (str)
        {
            case "����":
                return Enum_GM.ItemPlace.weapon;
            case "��":
                return Enum_GM.ItemPlace.clothes;
            case "�Ź�":
                return Enum_GM.ItemPlace.shoes;
            case "�Ͱ�":
                return Enum_GM.ItemPlace.earring;
            case "����":
                return Enum_GM.ItemPlace.ring;
            case "��":
                return Enum_GM.ItemPlace.pet;

            default:
                Debug.LogError("str ��ȯ �Ұ�");
                return Enum_GM.ItemPlace.weapon;
        }
    }
}
