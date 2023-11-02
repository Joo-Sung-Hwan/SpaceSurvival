using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

public enum CardKind
{
    bomb,
    bullet,
    laser,
    energyBolt,
    hp,
    speed,
    idle
}
public class SelectCardManager : MonoBehaviour
{
    
    public SelectCard selectcard;
    public Transform selectcard_parent;
    [HideInInspector] public List<SelectCard> sc_list = new List<SelectCard>();
    public TextAsset jsondata;
    public Dictionary<CardKind, List<CardData>> selectcarddata = new();
    public List<SelectCardManager.CardData> cardCheck_list = new();
    int card_child = 0;

    // Json데이터 받기 위해 stuct 구조 생성
    [System.Serializable]
    public struct CardData
    {
        public string title;
        public float change;
        public string rare;
        public string kind;
        public string category;
    }
    [System.Serializable]
    public struct SelectCardList
    {
        public List<CardData> Bomb;
        public List<CardData> Bullet;
        public List<CardData> Laser;
        public List<CardData> EnergyBolt;
        public List<CardData> Player;
    }

    SelectCardList sc_dic = new();
    void Start()
    {
        SetJsonData();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartSelectCard();
        }
    }

    // 카드 생성 함수
    public void CreateSelectCard()
    {
        SelectCard sc = GameManager.instance.pollingsystem.PoolingSelectCard(selectcard, selectcard_parent);
        sc.Init();
        sc_list.Add(sc);
        foreach (var item in sc_list)
        {
            if (item.gameObject.activeSelf)
            {
                card_child += 1;
            }
        }
        if(card_child < 3)
        {
            card_child = 0;
        }
    }

    // 카드 3개가 모두 애니메이션에 끝나야 버튼 이벤트 활성화하는 함수
    public void SetEnableButton()
    {
        if (card_child == 3)
        {
            foreach (var item in sc_list)
            {
                item.gameObject.GetComponent<Button>().enabled = true;
            }
            card_child = 0;
        }
    }

    // 카드 하나하나의 애니메이션이 끝나야 다음 카드 애니메이션이 돌게 하기위해 코루틴 사용
    IEnumerator DelayCardTime()
    {
        for (int i = 0; i < 3; i++)
        {
            CreateSelectCard();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartSelectCard()
    {
        StartCoroutine("DelayCardTime");
    }

    // Json 데이터를사용하기 위해 Dictionary에 저장
    public void SetJsonData()
    {
        sc_dic = JsonUtility.FromJson<SelectCardList>(jsondata.text);
        selectcarddata.Add(CardKind.bomb, sc_dic.Bomb);
        selectcarddata.Add(CardKind.bullet, sc_dic.Bullet);
        selectcarddata.Add(CardKind.laser, sc_dic.Laser);
        selectcarddata.Add(CardKind.energyBolt, sc_dic.EnergyBolt);
        selectcarddata.Add(CardKind.hp, sc_dic.Player);
    }
}
