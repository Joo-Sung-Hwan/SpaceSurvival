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
    [SerializeField] public Image[] backGroundImages;
    [SerializeField] public Image[] images;
    [SerializeField] public TMP_Text[] resultTexts;
    [SerializeField] public Button button;
    public Sequence mySequence;

    void Start()
    {
        mySequence = DOTween.Sequence();
        EneterExit();
        ResultMission();
        StartCoroutine(Events(ResultTime(), backGroundImages, images, ResultReWardTexts()));
    } 

    void Update()
    {

    }

    void ResultMission()
    {
        mySequence = DOTween.Sequence();
        if(GameManager.instance.player.missionCheck)
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
        int reward1 = GameManager.instance.gameUI.monsterIndex * 10;
        int reward2 = GameManager.instance.gameUI.monsterIndex;
        resultTexts[0].text = reward1.ToString();
        resultTexts[1].text = reward2.ToString();
        return resultTexts;
    }

    IEnumerator Events(TMP_Text text, Image[] bgImage, Image[] images, TMP_Text[] texts)
    {
        mySequence.Append(text.GetComponent<RectTransform>().DOScale(1.5f, 0.5f));
        yield return mySequence.WaitForCompletion();
        mySequence.Append(text.GetComponent<RectTransform>().DOScale(1f, 1f));
        List<Sequence> sequences = new();
        for(int i = 0; i < 2; i++)
        {
            sequences.Add(mySequence);
        }
        for(int i = 0; i < 2; i++)
        {
            sequences[i].Append(bgImage[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(500, 150), 0.5f, true));
            sequences[i].Append(images[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(150, 150), 0.5f, true));
            sequences[i].Append(texts[i].GetComponent<RectTransform>().DOScale(1.5f, 1.5f));
            yield return sequences[i].WaitForCompletion();
            yield return new WaitForSeconds(0.5f);
            sequences[i].Append(bgImage[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(400, 100), 1f, false));
            sequences[i].Append(images[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(100, 100), 1f, false));
            sequences[i].Append(texts[i].GetComponent<RectTransform>().DOScale(1f, 1f));
        }
    }

    public void EneterExit()
    {
        button.onClick.AddListener(FindObjectOfType<SceneChangeManger>().OnClickLobby);
    }
}
