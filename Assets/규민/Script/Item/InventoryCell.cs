using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [HideInInspector] public ItemData cellData = null;

    public Image itemImage;

    [SerializeField] GameObject itemButton;
    [Header("���â�� Cell������ ���")] 
    [SerializeField] GameObject icon;

    private void Start()
    {
        cellData = null;
    }

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
            itemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + cellData.itemStaticData.spriteName);
    }

    /// <summary>
    /// ��ư�� Ŭ������ �� �� ������ �ٲ��ִ� �Լ��� ȣ���ϴ� �Լ�
    /// </summary>
    public void OnSetDetail()
    {
        InventoryManager.Instance.SelectedItem = cellData;
    }
}
