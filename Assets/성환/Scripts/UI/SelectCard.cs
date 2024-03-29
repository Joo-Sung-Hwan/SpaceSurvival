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
    public void Init()
    {
        GetComponent<Button>().enabled = false;
        rand = Random.Range(1, 100);
        rand_index = rand < 70 ? "Normal" : rand < 90 ? "Rare" : "Unique";
        temp_list = new();
        // 장착한 무기에 대한 카드 Data 설정
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
            default:
                Weapon_Card(CardKind.idle);
                break;

        }
        gr = System.Enum.Parse<Grade>(rand_index);
        
        // 카드 등급에 따라 배경 설정
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
                GetComponent<Image>().sprite = sprite_list[2];
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
        // 장착한 무기에 따라 임시 리스트에 해당 무기 카드 데이터 저장
        if(cardKind != CardKind.idle)
        {
            foreach (var item in GameManager.instance.selectCardManager.selectcarddata[cardKind])
            {
                if (item.rare == grade)
                {
                    temp.Add(item);
                }
            }
        }

        // 장착한 무기에 상관없이 Player, 기본 무기인 Bullet 카드 데이터 저장
        foreach (var item in GameManager.instance.selectCardManager.selectcarddata[CardKind.hp])
        {
            if (item.rare == grade)
            {
                temp.Add(item);
            }
        }
        foreach (var item in GameManager.instance.selectCardManager.selectcarddata[CardKind.bullet])
        {
            if (item.rare == grade)
            {
                temp.Add(item);
            }
        }

        int rand0 = Random.Range(0, temp.Count);

        // 카드 3개가 생성될 때 중복된 카드가 나오지 않게 중복 값이 있으면 Init을 다시 불러줌
        if (GameManager.instance.selectCardManager.cardCheck_list.Contains(temp[rand0]))
        {
            Init();
        }

        // 카드 중복이 아니라면 카드에 데이터 삽입
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
                case "bullet":
                    image.sprite = weapon_list[(int)CardKind.bullet];
                    break;
                case "speed":
                    image.sprite = weapon_list[(int)CardKind.speed];
                    break;
                default:
                    image.sprite = weapon_list[(int)cardKind];
                    break;
            }
        }
    }

    // 카드 버튼 이벤트 실행 함수
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

    // 카드 데이터 인게임 적용
    public void SetAbility()
    {
        switch (cd.category)
        {
           case "Damage":
                switch (cd.kind)
                {
                    case "bomb":
                        // Hierachy 안에 있는 데이터 바꾸기 위해 생성할 때 저장한 풀링 시스템에 Queue에 접근하여 데이터 번경
                        foreach (var item in GameManager.instance.pollingsystem.bo_queue)
                        {
                            item.bd.BombAttack += cd.change;
                        }
                        break;
                    case "bullet":
                        foreach (var item in GameManager.instance.pollingsystem.b_queue)
                        {
                            item.weaponData.Damage += cd.change;
                        }
                        break;
                    case "laser":
                        foreach (var item in GameManager.instance.pollingsystem.lc_queue)
                        {
                            item.Attack += cd.change;
                        }
                        break;
                    case "energybolt":
                        foreach (var item in GameManager.instance.player.enegyTrans)
                        {
                            //item.weaponData.Damage += cd.change;
                        }
                        break;
                }
                break;
            case "AttackSpeed":
                switch (cd.kind)
                {
                    case "bomb":
                        GameManager.instance.player.BombCTime *= cd.change;
                        break;
                    case "bullet":
                        GameManager.instance.player.BulletCTime *= cd.change;
                        break;
                    case "laser":
                        GameManager.instance.player.LaserCTime *= cd.change;
                        break;
                }
                break;
            case "AttackRange":
                switch (cd.kind)
                {
                    case "bomb":
                        foreach (var item in GameManager.instance.pollingsystem.bo_queue)
                        {
                            item.bd.BombRange += cd.change;
                        }
                        break;
                }
                break;
            case "AttackAbility":
                foreach (var item in GameManager.instance.pollingsystem.b_queue)
                {
                    item.AttackAbility += (int)cd.change;
                }
                break;
            case "Last":
                foreach (var item in GameManager.instance.pollingsystem.l_queue)
                {
                    item.LaserLastTime *= cd.change;
                }
                break;
            case "Reflect":
                foreach (var item in GameManager.instance.pollingsystem.l_queue)
                {
                    item.ReflectMaxCount += (int)cd.change;
                }
                break;
            case "HP":
                GameManager.instance.player.definePD.MaxHp += cd.change;
                GameManager.instance.player.definePD.CurHp += cd.change;
                break;
            case "Speed":
                GameManager.instance.player.definePD.Speed += cd.change;
                break;
            case "Num":
                GameManager.instance.player.index += (int)cd.change;
                GameManager.instance.player.levelup = true;
                break;
        }
    }
    
    // 버튼 이벤트 활성화
    public void SetEnableChildButton()
    {
        GameManager.instance.selectCardManager.SetEnableButton();
    }
}
