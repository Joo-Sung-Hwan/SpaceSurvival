using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle_Main : MonoBehaviour
{
    [Header("�ڱ� �ڽ�")]
    [SerializeField] Toggle toggle;

    [Header("��� �̹���")]
    [SerializeField] Sprite normal_Image;
    [SerializeField] Sprite selected_Image;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        toggle.image.sprite = toggle.isOn ? selected_Image : normal_Image;
    }

    
}
