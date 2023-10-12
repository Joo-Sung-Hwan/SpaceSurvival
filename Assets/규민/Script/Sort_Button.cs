using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sort_Button : MonoBehaviour
{
    [SerializeField] Button btn;

    float coolTime = 2f;

    /// <summary>
    /// ���Ŀ� ��Ÿ���� ������ִ� �Լ�(Sort_Button)
    /// </summary>
    public void OnCoolDown()
    {
        btn.interactable = false;
        StartCoroutine(C_CoolDown());
    }

    IEnumerator C_CoolDown()
    {
        yield return new WaitForSeconds(coolTime);
        btn.interactable = true;
    }
}
