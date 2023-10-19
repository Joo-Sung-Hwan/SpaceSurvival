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
            StartSelectCard();
        }
    }

    public GameObject CreateSelectCard()
    {
        int card_index = 0;
        int rand = Random.Range(0, 100);
        card_index = rand < 60 ? 0 : rand < 90 ? 1 : 2;
        Debug.Log(rand);
        SelectCard sc = GameManager.instance.pollingsystem.PoolingSelectCard(selectcard[card_index], selectcard_parent);
        sc_list.Add(sc);
        //Instantiate(selectcard[card_index], selectcard_parent);
        return sc.gameObject;
    }

    IEnumerator DelayCardTime()
    {
        for (int i = 0; i < 3; i++)
        {
            CreateSelectCard().transform.SetAsLastSibling();
            yield return new WaitForSeconds(1f);
        }
    }

    public void StartSelectCard()
    {
        StartCoroutine("DelayCardTime");
    }
}
