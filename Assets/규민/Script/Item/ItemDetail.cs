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
        itemPlace_Txt.text = ivd.itemStaticData.place;
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
}
