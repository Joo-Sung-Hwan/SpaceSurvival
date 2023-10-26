using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Grade
{
    Normal,
    Rare,
    Unique
}
public enum Category
{
    Damage,
    AttackSpeed,
    AttackRange,
    AttackAbility,
    Last,
    Reflect,
    Player
}
public class SelectCard : MonoBehaviour
{
    [HideInInspector] public Grade gr;
    public List<Sprite> sprite_list = new List<Sprite>();
    public List<Sprite> weapon_list = new List<Sprite>();
    public Image image;
    public TMP_Text text;
    public List<SelectCardManager.CardData> cd_list = new();
    int rand;
    string rand_index;
    List<SelectCardManager.CardData> temp_list;
    SelectCardManager.CardData cd;
    public float ability_value;

    public void Init()
    {
        GetComponent<Image>().color = new Color(255f, 255f, 255f);
        rand = Random.Range(1, 100);
        rand_index = rand < 70 ? "Normal" : rand < 90 ? "Rare" : "Unique";
        temp_list = new();
        switch (GameManager.instance.player.player_weapon)
        {
            case PlayerWeapon.Laser:
                Weapon_Card(CardKind.laser);
                break;
            case PlayerWeapon.Bomb:
                Weapon_Card(CardKind.bomb);
                break;
            case PlayerWeapon.EnergyBolt:
                Weapon_Card(CardKind.energyBolt);
                break;

        }
        gr = System.Enum.Parse<Grade>(rand_index);
        
        switch (gr)
        {
            case Grade.Normal:
                gr = Grade.Normal;
                GetComponent<Image>().sprite = sprite_list[0];
                break;
            case Grade.Rare:
                GetComponent<Image>().sprite = sprite_list[1];
                break;
            case Grade.Unique:
                GetComponent<Image>().sprite = sprite_list[1];
                GetComponent<Image>().color = new Color(124f, 0f, 152f);
                break;
        }
    }

    public void Weapon_Card(CardKind cardKind)
    {
        switch (rand_index)
        {
            case "Normal":
            case "Rare":
            case "Unique":
                Setcard(cardKind, temp_list, rand_index);
                break;
        }
    }

    public void Setcard(CardKind cardKind, List<SelectCardManager.CardData> temp, string grade)
    {
        foreach (var item in GameManager.instance.selectCardManager.selectcarddata[cardKind])
        {
            if (item.rare == grade)
            {
                temp.Add(item);
            }
        }
        foreach (var item in GameManager.instance.selectCardManager.selectcarddata[CardKind.hp])
        {
            if (item.rare == grade)
            {
                temp.Add(item);
            }
        }
        int rand0 = Random.Range(0, temp.Count);
        if (GameManager.instance.selectCardManager.cardCheck_list.Contains(temp[rand0]))
        {
            Init();
        }
        else
        {
            GameManager.instance.selectCardManager.cardCheck_list.Add(temp[rand0]);
            cd = temp[rand0];
            text.text = cd.title;
            switch (cd.kind)
            {
                case "hp":
                    image.sprite = weapon_list[(int)CardKind.hp];
                    break;
                default:
                    image.sprite = weapon_list[(int)cardKind];
                    break;
            }
        }
    }
    public void OnclickCard()
    {
        Debug.Log(cd.change);
        foreach(var item in GameManager.instance.selectCardManager.sc_list)
        {
            item.gameObject.SetActive(false);
        }
        
        GameManager.instance.selectCardManager.cardCheck_list.Clear();
        GameManager.instance.selectCardManager.sc_list.Clear();
        GameManager.instance.isPause = false;
    }

    public void SetAbility()
    {
        switch (cd.category)
        {
            case "Damage":
                break;
            case "AttackSpeed":
                break;
            case "AttackRange":
                break;
            case "AttackAbility":
                break;
            case "Last":
                break;
            case "Reflect":
                break;
            case "Player":
                break;

        }
    }
    
}
