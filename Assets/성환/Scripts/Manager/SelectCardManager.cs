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
    hp
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
        public List<CardData> HP;
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

    public void CreateSelectCard()
    {
        SelectCard sc = GameManager.instance.pollingsystem.PoolingSelectCard(selectcard, selectcard_parent);
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

    public void SetJsonData()
    {
        sc_dic = JsonUtility.FromJson<SelectCardList>(jsondata.text);
        selectcarddata.Add(CardKind.bomb, sc_dic.Bomb);
        selectcarddata.Add(CardKind.bullet, sc_dic.Bullet);
        selectcarddata.Add(CardKind.laser, sc_dic.Laser);
        selectcarddata.Add(CardKind.energyBolt, sc_dic.EnergyBolt);
        selectcarddata.Add(CardKind.hp, sc_dic.HP);
    }
}
