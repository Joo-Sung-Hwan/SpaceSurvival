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
    
    [Header("���â�� Cell������ ���")] 
    [SerializeField] GameObject icon;

    /// <summary>
    /// �κ��丮 ĭ������ �̹����� �����ϴ� �Լ�
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
    /// ��ư�� Ŭ������ �� �� ������ �ٲ��ִ� �Լ��� ȣ���ϴ� �Լ�
    /// </summary>
    public void OnSetDetail()
    {
        InventoryManager.Instance.SelectedItem = cellData;
    }

    /// <summary>
    /// ��޿� ���� ������ �����ϴ� �Լ�
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
