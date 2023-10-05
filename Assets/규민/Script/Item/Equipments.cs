using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    [Header ("순서-무기,옷,신발,귀고리,반지,펫")]
    public List<InventoryCell> equipCells = new List<InventoryCell>();
    public Dictionary<Enum_GM.ItemPlace, InventoryCell> d_equipments = new Dictionary<Enum_GM.ItemPlace, InventoryCell>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < equipCells.Count; i++)
            d_equipments.Add((Enum_GM.ItemPlace)i, equipCells[i]);
    }

    /// <summary>
    /// 장착중인 아이템 표시
    /// </summary>
    public void SetEquipments()
    {
        if (!gameObject.activeInHierarchy)
            return;

        for (int i = 0; i < equipCells.Count; i++)
            d_equipments[(Enum_GM.ItemPlace)i].SetImage();
    }

    /// <summary>
    /// 장비 장착(버튼)
    /// </summary>
    public void OnEquip()
    {
        //간략화
        ItemData id = InventoryManager.Instance.SelectedItem;

        //장착중인 아이템이 있다면 해제
        if (d_equipments[StringToItemPlace(id.itemStaticData.place)].cellData != null)
            OnTakeOff();
        
        //장착
        d_equipments[StringToItemPlace(id.itemStaticData.place)].cellData = id;
        InventoryManager.Instance.inventoryDatas.Remove(id);
        InventoryManager.Instance.inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// 장비 장착 해제(버튼)
    /// </summary>
    public void OnTakeOff()
    {
        //간략화
        ItemData id = InventoryManager.Instance.SelectedItem;
        Enum_GM.ItemPlace place = StringToItemPlace(id.itemStaticData.place);

        //장착 해제
        InventoryManager.Instance.inventoryDatas.Add(d_equipments[place].cellData);
        d_equipments[place].cellData = null;
        InventoryManager.Instance.inventory_UI.OnCellsEnable();
    }

    /// <summary>
    /// json에 저장된 string값 -> Enum값 변경(장착 부위)
    /// </summary>
    /// <param name="str">Enum으로 변환할 부위</param>
    Enum_GM.ItemPlace StringToItemPlace(string str)
    {
        switch (str)
        {
            case "무기":
                return Enum_GM.ItemPlace.weapon;
            case "옷":
                return Enum_GM.ItemPlace.clothes;
            case "신발":
                return Enum_GM.ItemPlace.shoes;
            case "귀고리":
                return Enum_GM.ItemPlace.earring;
            case "반지":
                return Enum_GM.ItemPlace.ring;
            case "펫":
                return Enum_GM.ItemPlace.pet;

            default:
                Debug.LogError("str 변환 불가");
                return Enum_GM.ItemPlace.weapon;
        }
    }
}
