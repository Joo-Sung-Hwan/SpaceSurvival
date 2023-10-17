using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle_SelectMode : MonoBehaviour
{
    [SerializeField] GameObject blocker;
    [SerializeField] GameObject sort;
    [SerializeField] GameObject destroyButton;
    [SerializeField] Toggle toggle;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnToggleValueChange()
    {
        blocker.SetActive(toggle.isOn);
        sort.SetActive(!toggle.isOn);
        destroyButton.SetActive(toggle.isOn);
        InventoryManager.Instance.isSelectMode = toggle.isOn;

        //선택모드 해제시 선택된 아이템 리스트 삭제
        if (!toggle.isOn)
        {
            InventoryManager.Instance.SelectModeOff();
        }
    }
}
