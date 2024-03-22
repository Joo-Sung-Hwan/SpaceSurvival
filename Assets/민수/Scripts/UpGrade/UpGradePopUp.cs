using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpGradePopUp : MonoBehaviour
{
    [HideInInspector] public UpGradeManager gradeManager;
    private Action action = null;
    
    public void SetAction(Action action,UpGradeManager upGradeManager)
    {
        this.action = action;
        if (gradeManager == null)
        {
            gradeManager = upGradeManager;
        }
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
        }
        SetOff();
    }
    void SetOff()
    {
        action = null;
        gradeManager.isOn = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
