using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SelectCardManager : MonoBehaviour
{
    public SelectCard selectcard;
    public Transform selectcard_parent;
    [HideInInspector] public List<SelectCard> sc_list = new List<SelectCard>();


    public struct CardData
    {
        string text;
        float value;
        string rare;
    }

    void Start()
    {
        //selectcard = JsonUtility.
        //CardData cd = JsonUtility.FromJson<CardData>(selectjson);
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
}
