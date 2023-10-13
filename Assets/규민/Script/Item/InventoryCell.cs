using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [HideInInspector] public ItemData cellData = null;

    public Image itemImage;
    [SerializeField] Image cellFrame;
    [SerializeField] GameObject itemButton;
    
    [Header("장비창의 Cell에서만 사용")] 
    [SerializeField] GameObject icon;

    /// <summary>
    /// 인벤토리 칸에서의 이미지를 변경하는 함수
    /// </summary>
    public void SetImage()
    {
        bool isNoData = cellData == null || cellData.itemStaticData.name == "";
        
        itemButton.SetActive(!isNoData);
        if (icon)
            icon.SetActive(isNoData);

        if (!isNoData)
        {
            itemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + cellData.itemStaticData.spriteName);
            if (!icon)
                SetColor(cellData.rarity, cellFrame);
        }
        else if(!icon)
            cellFrame.color = Color.white;
    }

    /// <summary>
    /// 버튼을 클릭했을 때 상세 설명을 바꿔주는 함수를 호출하는 함수
    /// </summary>
    public void OnSetDetail()
    {
        InventoryManager.Instance.SelectedItem = cellData;
    }

    /// <summary>
    /// 등급에 따라 색깔을 변경하는 함수
    /// </summary>
    void SetColor(Enum_GM.Rarity rarity, Image img)
    {
        switch (rarity)
        {
            case Enum_GM.Rarity.legendary:
                img.color = new Color(1, 1, (74f / 255f));
                break;
            case Enum_GM.Rarity.unique:
                img.color = new Color((195f / 255f), 0, 255);
                break;
            case Enum_GM.Rarity.rare:
                img.color = new Color((61f / 255f), (167f / 255f), 1);
                break;
            case Enum_GM.Rarity.normal:
                img.color = Color.white;
                break;
            default:
                break;
        }
    }
}
