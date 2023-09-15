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
    /// 어떤 toggle이 꺼지고, 어떤 toggle이 켜지는지 받는 함수
    /// </summary>
    public void SetToggle(Enum_UI.Tg_Setting ts)
    {

    }


    /// <summary>
    /// 활성/비활성화된 Toggle의 이미지 크기를 바꿔주는 함수
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
