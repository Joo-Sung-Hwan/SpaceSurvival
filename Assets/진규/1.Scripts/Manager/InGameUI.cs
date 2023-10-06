using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;
    [SerializeField] private Image exp_Bar;
    [SerializeField] public Sequence mySequence;
    int min;
    float sec;
    float maxExp = 1054;
    float curExp = 0;
    float plus;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        //Exp();
        Exp1();
    }

    void Timer()
    {
        sec += Time.deltaTime;
        if(sec >= 60f)
        {
            min += 1;
            sec = 0;
        }
        timer.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
    }

    public void Exp()
    {
        mySequence = DOTween.Sequence();
        mySequence.Append(exp_Bar.GetComponent<RectTransform>().DOSizeDelta(new Vector2(13, 56.5f), 1f, false));

        if (Input.GetKeyDown(KeyCode.F1))
        {
            mySequence.Append(exp_Bar.GetComponent<RectTransform>().DOSizeDelta(new Vector2(13,56.5f),1f,false));
        }
    }

    void Exp1()
    {
        RectTransform exp_Rect = exp_Bar.GetComponent<RectTransform>();

        if (exp_Rect.rect.width >= maxExp)
            return;
        float exp = (260 / 13) / 100;
        exp = (float)System.Math.Truncate(exp * 13);
        if (Input.GetKeyDown(KeyCode.F1))
        {
            exp_Rect.sizeDelta += new Vector2(exp, 0);
        }
    }
}
