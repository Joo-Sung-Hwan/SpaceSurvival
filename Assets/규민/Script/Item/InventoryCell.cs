using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [HideInInspector] public InventoryData cellData;
    public Image itemImage;
    [SerializeField] GameObject itemButton;

    /// <summary>
    /// �κ��丮 ĭ������ �̹����� �����ϴ� �Լ�
    /// </summary>
    public void SetImage()
    {
        itemButton.SetActive(cellData.itemData.name != "");
        itemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + cellData.itemData.spriteName);
    }

    /// <summary>
    /// ��ư�� Ŭ������ �� �� ������ �ٲ��ִ� �Լ��� ȣ���ϴ� �Լ�
    /// </summary>
    public void OnSetDetail()
    {
        InventoryManager.Instance.itemDetail.SetDetails(cellData);
    }
}
