using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public abstract class ShopManager : MonoBehaviour
{
    [SerializeField] protected List<ItemScriptableData> scriptableDatas = new List<ItemScriptableData>();
    [SerializeField] protected Button buyButton;
    [SerializeField] protected GameObject maskObj;
    [SerializeField] protected BuyCheck buycheck;
    [SerializeField] protected GameObject failImage;
    [SerializeField] protected Image priceImage;
    [SerializeField] protected TMP_Text priceText;


    [SerializeField] protected Image product_Image;
    [SerializeField] protected TMP_Text information_Text;
    [SerializeField] protected Image check_Image;
    [SerializeField] protected TMP_Text check_Text;

    private void Awake()
    {
        buyButton.onClick.AddListener(() => OnClickBuyButtonDown());
    }
    protected virtual void OnClickBuyButtonDown()
    {
        maskObj.SetActive(true);
    }

    public ItemData RandomItem()
    {
        int rand = UnityEngine.Random.Range(0, scriptableDatas.Count);
        ItemScriptableData isd = scriptableDatas[rand];
        ItemStaticData newIsData = new ItemStaticData(isd.ItemName, isd.Place, isd.WeaponKind, isd.SpriteName, isd.Description);

        List<Item_Ability> newItemAbs = new List<Item_Ability>();

        //랜덤으로 어빌리티의 종류/수치를 결정함
        //Random.Range(최소 개수, 최대 개수+1)

        int valCount = Enum.GetValues(typeof(Enum_GM.abilityName)).Length;
        int randNum = UnityEngine.Random.Range(0, valCount);


        for (int i = 0; i < UnityEngine.Random.Range(2, 6); i++)
        {
            Item_Ability item_Ab = new Item_Ability();

            item_Ab.abilityName = (Enum_GM.abilityName)randNum;
            item_Ab.abilityValue = 1;
            item_Ab.abilityrarity = Enum_GM.Rarity.normal;


            newItemAbs.Add(item_Ab);
            //인수에 들어있는 람다식에 따라 리스트를 정렬
            //-> abilityValue값이 크면 앞으로 정렬
        }

        return new ItemData(newIsData, Enum_GM.Rarity.normal, newItemAbs, 0);
    }
}
