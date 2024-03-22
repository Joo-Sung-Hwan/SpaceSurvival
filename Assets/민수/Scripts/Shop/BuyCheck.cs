using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class BuyCheck : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    private Action action;


    private void Awake()
    {
        okButton.onClick.AddListener(() => OnOkButtonDown());
        cancelButton.onClick.AddListener(() => OnCancelButtonDown());
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
        gameObject.SetActive(false);
    }
}
