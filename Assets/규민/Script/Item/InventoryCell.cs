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
    /// 인벤토리 칸에서의 이미지를 변경하는 함수
    /// </summary>
    public void SetImage()
    {
        itemButton.SetActive(cellData.itemData.name != "");
        itemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + cellData.itemData.spriteName);
    }

    /// <summary>
    /// 버튼을 클릭했을 때 상세 설명을 바꿔주는 함수를 호출하는 함수
    /// </summary>
    public void OnSetDetail()
    {
        InventoryManager.Instance.itemDetail.SetDetails(cellData);
    }
}
