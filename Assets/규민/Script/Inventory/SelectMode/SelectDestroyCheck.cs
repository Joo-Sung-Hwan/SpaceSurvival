using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectDestroyCheck : MonoBehaviour
{
    [SerializeField] TMP_Text checkTxt;

    public void OnSetCheckTxt()
    {
        checkTxt.text = $"선택한 {InventoryManager.Instance.selectedCells.Count}개의 아이템을 버리시겠습니까?";
    }
}
