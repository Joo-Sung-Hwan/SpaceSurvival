using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class ResultManager : MonoBehaviour
{
    [SerializeField] public GameObject[] resultObjects;
    [SerializeField] public TMP_Text timeText;
    [SerializeField] public Transform[] parentTrans;
    [SerializeField] public Image[] images;
    [SerializeField] public TMP_Text[] resultTexts;
    [SerializeField] public Button button;
    public Sequence mySequence;
    //public UnityAction< > action;
    // Start is called before the first frame update
    void Start()
    {
        mySequence = DOTween.Sequence();
        ResultMission();
        StartCoroutine("Events",(ResultTime()));
        StartCoroutine("Events", (parentTrans, images, ResultReWardTexts()));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResultMission()
    {
        mySequence = DOTween.Sequence();
        if(!GameManager.instance.player.missionCheck)
        {
            resultObjects[0].gameObject.SetActive(true);
            mySequence.Append(resultObjects[0].GetComponent<RectTransform>().DOScaleX(1f, 1f));
        }
        else
        {
            resultObjects[1].gameObject.SetActive(true);
            mySequence.Append(resultObjects[1].GetComponent<RectTransform>().DOScaleX(1f, 1f));
        }
    }

    TMP_Text ResultTime()
    {
        TMP_Text time = GameManager.instance.gameUI.timer;
        timeText.text = time.text;
        return timeText;
    }

    TMP_Text[] ResultReWardTexts()
    {
        int reward1 = GameManager.instance.gameUI.monsterIndex * 100;
        int reward2 = GameManager.instance.gameUI.monsterIndex;
        resultTexts[0].text = reward1.ToString();
        resultTexts[1].text = reward2.ToString();
        return resultTexts;
    }


    IEnumerator Events(TMP_Text text)
    {
        mySequence.Append(text.GetComponent<RectTransform>().DOScale(1.5f, 0.5f));
        yield return mySequence.WaitForCompletion();
        mySequence.Append(text.GetComponent<RectTransform>().DOScale(1f, 1f));
    }

    IEnumerator Events(Transform[] trans, Image[] images, TMP_Text[] texts)
    {
        foreach (Transform item in trans)
        {
            mySequence.Append(item.GetComponent<RectTransform>().DOSizeDelta(new Vector2(500, 150), 0.5f, false));
        }
        foreach(Image item in images)
        {
            mySequence.Append(item.GetComponent<RectTransform>().DOSizeDelta(new Vector2(150, 150), 0.5f, false));
        }
        foreach(TMP_Text item in texts)
        {
            mySequence.Append(item.GetComponent<RectTransform>().DOScale(1.5f, 0.5f));
        }
        yield return mySequence.WaitForCompletion();
        foreach (Transform item in trans)
        {
            mySequence.Append(item.GetComponent<RectTransform>().DOSizeDelta(new Vector2(400, 100), 1f, false));
        }
        foreach (Image item in images)
        {
            mySequence.Append(item.GetComponent<RectTransform>().DOSizeDelta(new Vector2(100, 100), 1f, false));
        }
        foreach (TMP_Text item in texts)
        {
            mySequence.Append(item.GetComponent<RectTransform>().DOScale(1f, 0.5f));
        }
    }
}
