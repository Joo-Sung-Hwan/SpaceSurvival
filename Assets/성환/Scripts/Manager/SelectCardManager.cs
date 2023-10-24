using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SelectCardManager : MonoBehaviour
{
    public SelectCard selectcard;
    public Transform selectcard_parent;
    [HideInInspector] public List<SelectCard> sc_list = new List<SelectCard>();
    public TextAsset jsondata;

    [System.Serializable]
    public struct CardData
    {
        public string title;
        public float change;
        public string rare;
    }

    [System.Serializable]
    public struct CardKindData
    {
        public List<CardData> Damage;
        public List<CardData> AttackSpeed;
        public List<CardData> AttackRange;
    }

    [System.Serializable]
    public struct CardWeaponData
    {
        public List<CardKindData> Bomb;
        public List<CardKindData> Bullet;
        public List<CardKindData> Lasor;
        public List<CardKindData> EnergyBolt;
        public List<CardKindData> Player;

    }

    public struct SelectCardList
    {
        public List<CardWeaponData> SelectCardData;
    }

    public SelectCardList s_card_list = new();

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
        // Json 데이터 -> struct로 받아서 적용
        s_card_list = JsonUtility.FromJson<SelectCardList>(jsondata.text);
        
    }
}
