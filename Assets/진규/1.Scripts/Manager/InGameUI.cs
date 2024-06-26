using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Image exp_Bar;
    [SerializeField] private Image hp_Bar;
    [SerializeField] public TMP_Text timer;
    [SerializeField] public Button pauseButton;
    [SerializeField] public Material material;
    [SerializeField] public TMP_Text monsterIn;
    public Player player;
    public int min;
    public float sec;
    public int monsterIndex;
    public Image level_icon;
    public TMP_Text level_text;
    
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        level_text.text = GameManager.instance.player.definePD.Level.ToString();
        if (GameManager.instance.isPause == false)
            Timer();
        if (exp_Bar.GetComponent<RectTransform>().rect.width >= 967 || hp_Bar.GetComponent<RectTransform>().rect.width <= 0)
            return;
        CalculationBar(exp_Bar, player.definePD.CurExp, player.definePD.MaxExp, 967, 56.5f);
        CalculationBar(hp_Bar, player.definePD.CurHp, player.definePD.MaxHp, 193, 32f);
    }

    // 타이머 적용
    void Timer()
    {
        sec += Time.deltaTime;
        if(sec >= 60f)
        {
            min += 1;
            sec = 0;
        }
        timer.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
        MonsterIndex(monsterIndex);
    }

    // 경험치 및 체력bar 코드 수치 적용
    void CalculationBar(Image image, float curValue, float maxValue, int width, float height)
    {
        RectTransform targetRect = image.GetComponent<RectTransform>();
        Sequence MySequence = DOTween.Sequence();
        float getValue = curValue / maxValue;
        float changeValue = getValue * width;
        float Value = (float)System.Math.Truncate(changeValue);
        MySequence.Append(targetRect.DOSizeDelta(new Vector2(Value, height), 1f, false).SetLink(gameObject));
    }

    public void MonsterIndex(int index)
    {
        monsterIn.text = index.ToString();
    }
}
