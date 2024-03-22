using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class BuyCheck : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private GameObject maskObj;
    [SerializeField] private TMP_Text infoText;

    private Action action;


    private void Awake()
    {
        okButton.onClick.AddListener(() => OnOkButtonDown());
        cancelButton.onClick.AddListener(() => OnCancelButtonDown());
    }
    public void SetInfoText(int cost)
    {
        infoText.text += $"\n{cost}개를 사용하여 구매하시겠습니까?";
    }
    public void SetBuyCheckData(Action action, Sprite sprite)
    {
        image.sprite = sprite;

        if (this.action == null)
        {
            this.action = action;
        }
    }
    void OnOkButtonDown()
    {
        action();
        action = null;
        gameObject.SetActive(false);
    }
    void OnCancelButtonDown()
    {
        action = null;
        maskObj.SetActive(false);
        gameObject.SetActive(false);
    }
}
