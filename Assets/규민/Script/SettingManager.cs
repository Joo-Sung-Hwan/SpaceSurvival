using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] Toggle[] toggles;

    Enum_UI.Tg_Setting preTg;
    Enum_UI.Tg_Setting curTg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// � toggle�� ������, � toggle�� �������� �޴� �Լ�
    /// </summary>
    public void SetToggle(Enum_UI.Tg_Setting ts)
    {

    }


    /// <summary>
    /// Ȱ��/��Ȱ��ȭ�� Toggle�� �̹��� ũ�⸦ �ٲ��ִ� �Լ�
    /// </summary>
    void ChangeToggle()
    {
        if (curTg > preTg)
        {

        }
        else
        {

        }
    }

}
