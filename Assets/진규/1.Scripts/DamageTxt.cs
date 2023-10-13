using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class DamageTxt : MonoBehaviour
{
    public Sequence mySequence;
    float endtime;
    float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
            time = 0;
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * 0.5f * Time.deltaTime);
        DamageTxtDoTween();
    }

    void DamageTxtDoTween()
    {
        TMP_Text text = transform.GetComponent<TMP_Text>();
        mySequence = DOTween.Sequence().SetAutoKill(false);
        mySequence.Append(text.GetComponent<RectTransform>().DOScale(1f, 2f));
        mySequence.Append(text.DOFade(0,1f));
    }
}
