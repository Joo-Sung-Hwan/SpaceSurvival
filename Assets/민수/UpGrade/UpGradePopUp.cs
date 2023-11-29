using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpGradePopUp : MonoBehaviour
{
    private Action action = null;

    public void SetAction(Action action)
    {
        this.action = action;
    }
    public void OnButtonDown(bool ison)
    {
        if (ison)
        {
            if (action == null)
            {
                Debug.Log("액션 없음");
                return;
            }
            action();
            SetOff();
        }
        else
        {
            SetOff();
        }
    }
    void SetOff()
    {
        action = null;
        gameObject.SetActive(false);
    }
}
