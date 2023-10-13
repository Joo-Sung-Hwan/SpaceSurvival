using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetail : MonoBehaviour
{
    [SerializeField] TMP_Text itemPlace_Txt;
    [SerializeField] TMP_Text itemName_Txt;
    [SerializeField] TMP_Text itemDesc_Txt;
    [SerializeField] List<TMP_Text> ability_Txt;
    [SerializeField] Image itemImage;

    [SerializeField] GameObject mask;

    [Header("DestroyCheck")]
    [SerializeField] GameObject dc;
    [SerializeField] Image dc_itemImage;
    [SerializeField] List<TMP_Text> dc_ability_Txt;

    [HideInInspector] public ItemData itemdata;
    [HideInInspector] public int cellIndex;

    private void Start()
    {
        //d_Color.Add(Enum_GM.Color.yellow, new Color(1, 1, (74f / 255f)));
        //d_Color.Add(Enum_GM.Color.purple, new Color((195f / 255f), 0, 255));
        //d_Color.Add(Enum_GM.Color.blue, new Color((61f/255f), (167f/255f), 1));
        //d_Color.Add(Enum_GM.Color.green, Color.green);
        //d_Color.Add(Enum_GM.Color.white, Color.white);
    }

    /// <summary>
    /// 상세 설명을 바꿔주는 함수
    /// </summary>
    /// <param name="ivd"></param>
    public void SetDetails(ItemData ivd)
    {
        itemdata = ivd;
        itemPlace_Txt.text = ivd.itemStaticData.place.ToString();
        itemName_Txt.text = ivd.itemStaticData.name;
        SetColor(ivd.rarity, itemName_Txt);

        itemDesc_Txt.text = ivd.itemStaticData.description;

        int index = 0;
        foreach (var abil in ivd.abilities)
        {
            ability_Txt[index].text = abil.abilityName + " + " + abil.abilityValue + "%";
            SetColor(abil.abilityrarity, ability_Txt[index++]);
        }

        itemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + ivd.itemStaticData.spriteName);
    }

    /// <summary>
    /// 등급에 따라 텍스트 색깔을 변경하는 함수
    /// </summary>
    void SetColor(Enum_GM.Rarity rarity, TMP_Text Txt)
    {
        switch (rarity)
        {
            case Enum_GM.Rarity.legendary:
                Txt.color = new Color(1, 1, (74f / 255f));
                break;
            case Enum_GM.Rarity.unique:
                Txt.color = new Color((195f / 255f), 0, 255);
                break;
            case Enum_GM.Rarity.rare:
                Txt.color = new Color((61f / 255f), (167f / 255f), 1);
                break;
            case Enum_GM.Rarity.normal:
                Txt.color = Color.white;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// destroyCheck의 정보를 변경하는 함수 (Destroy_Button)
    /// </summary>
    public void OnDestroyCheck()
    {
        dc_itemImage.sprite = itemImage.sprite;
        for (int i = 0; i < ability_Txt.Count; i++)
        {
            dc_ability_Txt[i].text = ability_Txt[i].text;
            dc_ability_Txt[i].color = ability_Txt[i].color;
        }
    }

    /// <summary>
    /// mask가 대상을 비활성화하는 함수(Item_Mask)
    /// -> 확인창이 켜져있을 경우 확인창 비활성화 후 다시 마스크 활성화, 확인창이 꺼져있을 경우 정보창 비활성화
    /// </summary>
    public void OnMask()
    {
        if (dc.activeInHierarchy)
        {
            dc.SetActive(false);
            mask.SetActive(true);
        }
        else
            gameObject.SetActive(false);
            
    }
}
