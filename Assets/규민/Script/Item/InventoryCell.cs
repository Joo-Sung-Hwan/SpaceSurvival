using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [HideInInspector] public ItemData cellData = null;

    public Image itemImage;

    [SerializeField] GameObject itemButton;
    [Header("장비창의 Cell에서만 사용")] 
    [SerializeField] GameObject icon;

    private void Start()
    {
        cellData = null;
    }

    /// <summary>
    /// 인벤토리 칸에서의 이미지를 변경하는 함수
    /// </summary>
    public void SetImage()
    {
        bool isData = cellData == null || cellData.itemStaticData.name == "";
        itemButton.SetActive(!isData);
        if (icon)
            icon.SetActive(isData);
        
        if (!isData)
            itemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + cellData.itemStaticData.spriteName);
    }

    /// <summary>
    /// 버튼을 클릭했을 때 상세 설명을 바꿔주는 함수를 호출하는 함수
    /// </summary>
    public void OnSetDetail()
    {
        InventoryManager.Instance.SelectedItem = cellData;
    }
}
