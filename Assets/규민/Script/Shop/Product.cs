using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Product : MonoBehaviour
{
    [SerializeField] Image product_Image;
    [SerializeField] TMP_Text information_Text;
    [SerializeField] Image check_Image;
    [SerializeField] TMP_Text check_Text;

    public void OnClick()
    {
        check_Image.sprite = product_Image.sprite;
        check_Text.text = information_Text.text;
    }
}
