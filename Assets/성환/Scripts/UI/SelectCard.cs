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
    GameObject equip_weapon;
    GameObject equip_weapon2;
    public void Init()
    {
        switch (GameManager.instance.player.player_weapon)
        {
            case PlayerWeapon.NormalBomb:
            case PlayerWeapon.MagnetBomb:
            case PlayerWeapon.WebBomb:
            case PlayerWeapon.FireBomb:
                equip_weapon = GameManager.instance.player.bomb.gameObject;
                break;
            case PlayerWeapon.Laser:
                equip_weapon = GameManager.instance.player.laser.gameObject;
                break;
            case PlayerWeapon.EnergyBolt:
                equip_weapon = GameManager.instance.player.fxmanager.gameObject;
                break;
        }
        equip_weapon2 = GameManager.instance.player.bullet.gameObject;
        GetComponent<Button>().enabled = false;
        GetComponent<Image>().color = new Color(255f, 255f, 255f);
        rand = Random.Range(1, 100);
        rand_index = rand < 70 ? "Normal" : rand < 90 ? "Rare" : "Unique";
        temp_list = new();
        switch (GameManager.instance.player.player_weapon)
        {
            case PlayerWeapon.Laser:
                Weapon_Card(CardKind.laser);
                break;
            case PlayerWeapon.NormalBomb:
            case PlayerWeapon.FireBomb:
            case PlayerWeapon.WebBomb:
            case PlayerWeapon.MagnetBomb:
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
        foreach(var item in GameManager.instance.selectCardManager.sc_list)
        {
            item.gameObject.SetActive(false);
        }
        SetAbility();
        GameManager.instance.selectCardManager.cardCheck_list.Clear();
        GameManager.instance.selectCardManager.sc_list.Clear();
        GameManager.instance.isPause = false;
    }

    public void SetAbility()
    {
        switch (cd.category)
        {
           case "Damage":
                switch (cd.kind)
                {
                    case "bomb":
                        GetComponent<Bomb>().bd.BombAttack *= cd.change;
                        break;
                    case "bullet":
                        GetComponent<Bullet>().Attack *= cd.change;
                        break;
                    case "laser":
                        GetComponent<LaserChild>().Attack *= cd.change;
                        break;
                    case "energybolt":
                        GetComponent<FxManager>().fd.Attack *= cd.change;
                        break;
                }
                break;
            case "AttackSpeed":
                switch (cd.kind)
                {
                    case "bomb":
                        GetComponent<Player>().BombCTime = cd.change;
                        break;
                    case "bullet":
                        GetComponent<Player>().BulletCTime = cd.change;
                        break;
                    case "laser":
                        GetComponent<Player>().LaserCTime = cd.change;
                        break;
                }
                break;
            case "AttackRange":
                switch (cd.kind)
                {
                    case "bomb":
                        //GetComponent<Bomb>(). = cd.change;
                        break;
                }
                break;
            case "AttackAbility":
                GetComponent<Bullet>().AttackAbility += (int)cd.change;
                break;
            case "Last":
                GetComponent<Laser>().LaserLastTime *= cd.change;
                break;
            case "Reflect":
                //GameManager.instance.ReflectMaxCount += (int)cd.change;
                break;
            case "Player":
                GetComponent<Player>().definePD.MaxHp += cd.change;
                GetComponent<Player>().definePD.CurHp += cd.change;
                break;
            case "NUM":
                GameManager.instance.player.index += (int)cd.change;
                break;
        }
    }
    
    // 버튼 이벤트 활성화
    public void SetEnableChildButton()
    {
        GameManager.instance.selectCardManager.SetEnableButton();
    }
}
