using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardManager : MonoBehaviour
{
    public SelectCard[] selectcard;
    public Transform selectcard_parent;
    [HideInInspector] public List<SelectCard> sc_list = new List<SelectCard>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine("DelayCardTime");
        }
    }

    public void CreateSelectCard()
    {
        int rand = Random.Range(0, 100);
        int card_index = rand < 60 ? 0 : 1;
        SelectCard sc = GameManager.instance.pollingsystem.PoolingSelectCard(selectcard[card_index], selectcard_parent);
        sc_list.Add(sc);
        //Instantiate(selectcard[card_index], selectcard_parent);
    }

    IEnumerator DelayCardTime()
    {
        for (int i = 0; i < 3; i++)
        {
            CreateSelectCard();
            yield return new WaitForSeconds(1f);
        }
    }
}
