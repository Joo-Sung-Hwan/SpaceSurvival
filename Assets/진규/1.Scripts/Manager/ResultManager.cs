using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        ResultMission();
        ResultTime();
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
            mySequence.Append(resultObjects[0].GetComponent<RectTransform>().DOScaleX(1f, 1.5f));
        }
        else
        {
            resultObjects[1].gameObject.SetActive(true);
            mySequence.Append(resultObjects[1].GetComponent<RectTransform>().DOScaleX(1f, 1.5f));
        }
    }

    void ResultTime()
    {
        TMP_Text time = GameManager.instance.gameUI.timer;
        timeText.text = time.text;
        mySequence = DOTween.Sequence();
        mySequence.Append(timeText.GetComponent<RectTransform>().DOScale(1f, 1.5f));
    }

    void ResultReWard()
    {

    }
}
