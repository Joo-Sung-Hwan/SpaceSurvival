using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class DamageTxt : MonoBehaviour
{
    public Sequence mySequence;
    float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.3f)
        {
            time = 0;
            gameObject.SetActive(false);
        }
        transform.Translate(Vector2.up * 1.5f * Time.deltaTime);
        DamageTxtDoTween();
    }

    // 데미지 텍스트 Dotween사용 코드
    void DamageTxtDoTween()
    {
        TMP_Text text = transform.GetComponent<TMP_Text>();
        mySequence = DOTween.Sequence();
        mySequence.Append(text.GetComponent<RectTransform>().DOScale(1f, 2f));
    }
}
