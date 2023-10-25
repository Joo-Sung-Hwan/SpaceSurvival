using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyButton : MonoBehaviour
{
    [SerializeField] Button btn;

    private void Update() => btn.enabled = InventoryManager.Instance.selectedCells.Count != 0;
}
