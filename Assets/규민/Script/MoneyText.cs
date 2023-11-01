using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    [SerializeField] TMP_Text money_Text;

    // Start is called before the first frame update
    void Start()
    {
        money_Text.text = InventoryManager.Instance.Gold.ToString();
    }
}
