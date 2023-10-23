using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView_Combat : MonoBehaviour, IDragHandler , IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Scrollbar scr;
    [SerializeField] Transform content_Trans;

    bool isDrag = false;
    float curValue;
    float targetValue;

    //�Ÿ� (scroll���� ���� �гη� �Ѿ�� �� �����ϴ� value�� ��ġ)
    float distance;

    private void Start()
    {
        int childcnt = content_Trans.childCount;
        distance = 1f / (childcnt - 1);
    }

    private void Update()
    {
        if (!isDrag)
            scr.value = Mathf.Lerp(scr.value, targetValue, 0.1f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        curValue = TargetIndex() * distance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        //����� �г��� value�� �Ѱ� �ڵ����� ��ũ���ϰ� �ϴ� �Լ�
        targetValue = TargetIndex() * distance;

        //tagertValue�� �ٲ��� �ʾƵ�(��ũ���� ª�� �ص�) ������ �ѱ�� �Ѿ
        if (curValue == targetValue)
        {
            //���������� �� ��(�������� �̵�), �ǳ� ����
            if (eventData.delta.x > 15 && curValue > distance)
            {
                targetValue -= distance;
            }
            //�������� �� ��(���������� �̵�), �ǳ� ����
            else if (eventData.delta.x < -15 && curValue + distance < 1.01f)
            {
                targetValue += distance;
            }
        }
    }

    /// <summary>
    /// �� ����� �г��� index�� ��ȯ�ϴ� �Լ�
    /// </summary>
    int TargetIndex()
    {
        //�� ��° �г����� �˱����� ����
        int targetIndex = (int)(scr.value / distance);
        //������ (��� �гο� �� ������� �˱����� ����)
        float remainder = scr.value % distance;

        //���� �гο� �� �����ٸ� index ����
        if (remainder > distance * 0.5f)
            targetIndex++;

        //��ũ�� ����
        return targetIndex;
    }
}
