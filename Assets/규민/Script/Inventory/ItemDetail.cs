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
    [SerializeField] private UpGradeManager upGradeManager;

    public GameObject mask;

    [Header("DestroyCheck")]
    [SerializeField] GameObject dc;
    [SerializeField] Image dc_itemImage;
    [SerializeField] List<TMP_Text> dc_ability_Txt;

    [Header("")]
    [SerializeField] GameObject buyFail;

    [HideInInspector] public ItemData itemdata;
    [HideInInspector] public int cellIndex;

    public static Color yellow = new Color(1, 1, (74f / 255f));
    public static Color purple = new Color((195f / 255f), 0, 255);
    public static Color blue = new Color((61f/255f), (167f/255f), 1);
    public static Color green = Color.green;
    public static Color white = Color.white;


    /// <summary>
    /// 상세 설명을 바꿔주는 함수
    /// </summary>
    /// <param name="ivd"></param>
    public void SetDetails(ItemData ivd)
    {
        itemdata = ivd;
        itemPlace_Txt.text = ivd.itemStaticData.place.ToString();
        if (ivd.itemStaticData.itemLevel != 0)
        {
            itemName_Txt.text = $"{ivd.itemStaticData.name}+{ivd.itemStaticData.itemLevel}";
        }
        else
        {
            itemName_Txt.text = ivd.itemStaticData.name;
        }
        SetColor(ivd.rarity, itemName_Txt);

        itemDesc_Txt.text = ivd.itemStaticData.description;

        for (int i = 0; i < ability_Txt.Count; i++)
        {
            if (i + 1 <= ivd.abilities.Count)
            {
                ability_Txt[i].gameObject.SetActive(true);
                ability_Txt[i].text = AbEnumToString(ivd.abilities[i].abilityName) + " + " + ivd.abilities[i].abilityValue + "%";
                SetColor(ivd.abilities[i].abilityrarity, ability_Txt[i]);
            }
            else
            {
                ability_Txt[i].text = "";
                ability_Txt[i].gameObject.SetActive(false);
            }
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
                Txt.color = yellow;
                break;
            case Enum_GM.Rarity.unique:
                Txt.color = purple;
                break;
            case Enum_GM.Rarity.rare:
                Txt.color = blue;
                break;
            case Enum_GM.Rarity.normal:
                Txt.color = white;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// abilityName을 string으로 바꿔주는 함수
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string AbEnumToString(Enum_GM.abilityName name)
    {
        switch (name)
        {
            case Enum_GM.abilityName.damage:
                return "공격력";
            case Enum_GM.abilityName.range:
                return "공격 범위";
            case Enum_GM.abilityName.attackSpeed:
                return "공격 속도";
            case Enum_GM.abilityName.speed:
                return "이동 속도";
            default:
                Debug.LogError("오류 : itemdetail 확인");
                return "공격력";
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

    public void OnRandomBoxResult(int price)
    {
        if (InventoryManager.Instance.Gold < price)
        {
            buyFail.SetActive(true);
            return;
        }
        InventoryManager.Instance.Gold -= price;
        gameObject.SetActive(true);
        ItemData id = InventoryManager.Instance.RandomItem();
        SetDetails(id);
        InventoryManager.Instance.AddItem(id);
    }
    public void OnMyItemDataToUpGrade()
    {
        InventoryManager.Instance.IsUpGrade = true;
        upGradeManager.gameObject.SetActive(true);
        upGradeManager.SetCurItemInfo(InventoryManager.Instance.SelectedItem);
        gameObject.SetActive(false);
    }
}
