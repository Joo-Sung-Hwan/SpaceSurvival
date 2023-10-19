using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Grade
{
    Normal,
    Rare,
    Unique
}
public abstract class SelectCard : MonoBehaviour
{
    [HideInInspector] public Grade gr;
    public abstract void Init();

    public virtual void OnclickCard()
    {
        foreach(var item in GameManager.instance.selectCardManager.sc_list)
        {
            item.gameObject.SetActive(false);
        }
        GameManager.instance.selectCardManager.sc_list.Clear();
        GameManager.instance.isPause = false;
    }
}
