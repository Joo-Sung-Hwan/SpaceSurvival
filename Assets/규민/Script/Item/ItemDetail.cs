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
    public void SetDetails(InventoryData ivd)
    {
        itemPlace_Txt.text = ivd.itemData.place;
        itemName_Txt.text = ivd.itemData.name;
        itemDesc_Txt.text = ivd.itemData.description;

        int index = 0;
        foreach (var abil in ivd.abilities)
        {
            ability_Txt[index].text = abil.abilityName + " + " + abil.abilityValue + "%";
            SetAbColor(abil, ability_Txt[index++]);
        }

        itemImage.sprite = Resources.Load<Sprite>("ItemIcons/" + ivd.itemData.spriteName);
    }

    /// <summary>
    /// 어빌리티 등급에 따라 텍스트 색깔을 변경하는 함수
    /// </summary>
    /// <param name="ab"></param>
    /// <param name="ab_Txt"></param>
    void SetAbColor(Item_Ability ab, TMP_Text ab_Txt)
    {
        switch (ab.abilityrarity)
        {
            case Enum_GM.Rarity.legendary:
                ab_Txt.color = new Color(1, 1, (74f / 255f));
                break;
            case Enum_GM.Rarity.unique:
                ab_Txt.color = new Color((195f / 255f), 0, 255);
                break;
            case Enum_GM.Rarity.rare:
                ab_Txt.color = new Color((61f / 255f), (167f / 255f), 1);
                break;
            case Enum_GM.Rarity.normal:
                ab_Txt.color = Color.white;
                break;
            default:
                break;
        }
    }
}
