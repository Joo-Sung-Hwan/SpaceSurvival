using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectDestroyCheck : MonoBehaviour
{
    [SerializeField] TMP_Text checkTxt;

    public void OnSetCheckTxt()
    {
        checkTxt.text = $"������ {InventoryManager.Instance.selectedCells.Count}���� �������� �����ðڽ��ϱ�?";
    }
}
