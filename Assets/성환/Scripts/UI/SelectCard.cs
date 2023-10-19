using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public void Init()
    {
        GetComponent<Image>().color = new Color(255f, 255f, 255f);
        int rand = Random.Range(0, 100);
        int rand_index = rand < 60 ? 0 : rand < 80 ? 1 : 2;
        switch (rand_index)
        {
            case 0:
                gr = Grade.Normal;
                GetComponent<Image>().sprite = sprite_list[0];
                Debug.Log("Normal");
                break;
            case 1:
                gr = Grade.Rare;
                GetComponent<Image>().sprite = sprite_list[1];
                Debug.Log("Rare");
                break;
            case 2:
                gr = Grade.Unique;
                GetComponent<Image>().sprite = sprite_list[1];
                GetComponent<Image>().color = new Color(124f, 0f, 152f);
                Debug.Log("Unique");
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
    }
}
