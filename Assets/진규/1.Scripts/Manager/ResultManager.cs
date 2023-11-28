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
    int resultGold;

    void Start()
    {
        resultGold = 0;
        mySequence = DOTween.Sequence();
        ResultMission();
        Envents(ResultTime());
        Envents(backGroundImages, images, ResultReWardTexts());
        EneterExit();
    } 

    void Update()
    {

    }

    void ResultMission()
    {
        if(GameManager.instance.player.missionCheck)
        {
            resultObjects[0].gameObject.SetActive(true);
            mySequence.Append(resultObjects[0].GetComponent<RectTransform>().DOScaleX(1f, 0.3f)).SetLink(gameObject);
        }
        else
        {
            resultObjects[1].gameObject.SetActive(true);
            mySequence.Append(resultObjects[1].GetComponent<RectTransform>().DOScaleX(1f, 0.3f)).SetLink(gameObject);
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
        int reward1 = GameManager.instance.gameUI.monsterIndex + (int)(GameManager.instance.gameUI.sec * 0.1f);
        int reward2 = GameManager.instance.gameUI.monsterIndex;
        resultGold = reward1;
        resultTexts[0].text = reward1.ToString();
        resultTexts[1].text = reward2.ToString();
        return resultTexts;
    }

    void Envents(TMP_Text text)
    {
        mySequence.Append(text.GetComponent<RectTransform>().DOScale(1.5f, 0.1f)).SetLink(gameObject)
            .WaitForCompletion();
        mySequence.Append(text.GetComponent<RectTransform>().DOScale(1f, 0.1f)).SetLink(gameObject);
    }

    void Envents(Image[] bgImage, Image[] images, TMP_Text[] texts)
    {
        List<Sequence> sequences = new();
        for (int i = 0; i < 2; i++)
        {
            sequences.Add(mySequence);
        }
        for (int i = 0; i < 2; i++)
        {
            sequences[i].Append(bgImage[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(500, 150), 0.2f, true))
                .Append(images[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(150, 150), 0.1f, true))
                .Append(texts[i].GetComponent<RectTransform>().DOScale(1.5f, 0.1f)).SetLink(gameObject)
                .WaitForCompletion();
            sequences[i].Append(bgImage[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(400, 100), 0.1f, false))
                .Append(images[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(100, 100), 0.1f, false))
                .Append(texts[i].GetComponent<RectTransform>().DOScale(1f, 0.1f)).SetLink(gameObject);
        }
    }

    public void EneterExit()
    {
        FindObjectOfType<SceneChangeManger>().SetGold(resultGold);
        button.onClick.AddListener(FindObjectOfType<SceneChangeManger>().OnClickLobby);
        ObjectPoolSystem.ObjectPoolling<Player>.AllClear();
    }
}
