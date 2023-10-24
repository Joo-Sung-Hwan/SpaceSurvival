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
    int rand_index;
    int rand1;
    List<int> rand_list = new();
    List<List<int>> rand2_list = new();
    public void Init()
    {
        SelectCardManager.SelectCardList sl = GameManager.instance.selectCardManager.s_card_list;
        GetComponent<Image>().color = new Color(255f, 255f, 255f);
        SetRand();
        //int rand2 = Random.Range(0, sl.SelectCardData[0].Bomb[rand1].);
        
        if (GameManager.instance.player.player_weapon == PlayerWeapon.Bomb)
        {
            image.sprite = weapon_list[0];
            switch (rand1)
            {
                case 0:
                    text.text = sl.SelectCardData[0].Bomb[0].Damage[rand_index].title;
                    // string 값을 enum 값으로 변환
                    gr = System.Enum.Parse<Grade>(sl.SelectCardData[0].Bomb[0].Damage[rand_index].rare);
                    
                    break;
                case 1:
                    text.text = sl.SelectCardData[0].Bomb[0].AttackSpeed[rand_index].title;
                    // string 값을 enum 값으로 변환
                    gr = System.Enum.Parse<Grade>(sl.SelectCardData[0].Bomb[0].AttackSpeed[rand_index].rare);
                    break;
                case 2:
                    text.text = sl.SelectCardData[0].Bomb[0].AttackRange[rand_index].title;
                    // string 값을 enum 값으로 변환
                    gr = System.Enum.Parse<Grade>(sl.SelectCardData[0].Bomb[0].AttackRange[rand_index].rare);
                    break;
            }
            
        }
        else if(GameManager.instance.player.player_weapon == PlayerWeapon.Laser)
        {
            image.sprite = weapon_list[2];
            switch (rand1)
            {
                case 0:
                    text.text = sl.SelectCardData[0].Lasor[0].Damage[rand_index].title;
                    // string 값을 enum 값으로 변환
                    gr = System.Enum.Parse<Grade>(sl.SelectCardData[0].Lasor[0].Damage[rand_index].rare);
                    break;
                case 1:
                    text.text = sl.SelectCardData[0].Lasor[0].AttackSpeed[rand_index].title;
                    // string 값을 enum 값으로 변환
                    gr = System.Enum.Parse<Grade>(sl.SelectCardData[0].Lasor[0].AttackSpeed[rand_index].rare);
                    break;
                case 2:
                    text.text = sl.SelectCardData[0].Lasor[0].AttackRange[rand_index].title;
                    // string 값을 enum 값으로 변환
                    gr = System.Enum.Parse<Grade>(sl.SelectCardData[0].Lasor[0].AttackRange[rand_index].rare);
                    break;
            }
                cd_list.Add(sl.SelectCardData[0].Bomb[0].Damage[rand_index]);
            }
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

    public void OnclickCard()
    {
        foreach(var item in GameManager.instance.selectCardManager.sc_list)
        {
            item.gameObject.SetActive(false);
        }
        GameManager.instance.selectCardManager.sc_list.Clear();
        GameManager.instance.isPause = false;
        rand_list.Clear();
        rand2_list.Clear();
    }

    public void SetRand()
    {
        rand = Random.Range(1, 100);
        rand_index = rand < 70 ? 0 : rand < 90 ? 1 : 2;
        rand1 = Random.Range(0, 3);
        List<int> tmp_list = new();
        

        for (int i = 0; i < 2; i++)
        {
            for(int j = 0; j < rand2_list.Count; j++)
            {
                if((rand2_list[j][0] == rand1) && (rand2_list[j][1] == rand_index))
                {
                    SetRand();
                }
                else
                {
                    rand_list.Add(rand1);
                    rand_list.Add(rand_index);
                    tmp_list.Add(rand1);
                    tmp_list.Add(rand_index);
                    rand2_list.Add(rand_list);
                    tmp_list.Clear();
                }
            }
        }
        
        Debug.Log($"rand1 = {rand1}, rand_index = {rand_index}");
    }
}
