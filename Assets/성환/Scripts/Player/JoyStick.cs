using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler,IEndDragHandler,IDragHandler
{
    // Lever 속성 값
    [Header("Lever 속성 값")]
    [SerializeField] private RectTransform lever;
    // lever 최대범위 - 조이스틱 밖으로 나가지 않게 하는 범위
    float lever_range = 100f;
    [HideInInspector] public Vector2 input_vector;

    // Player 속성 값
    public Player player;
    bool isDrag;

    // Player 움직이는 속도
    [HideInInspector] public float Speed { get; set; }

    void Start()
    {
        Speed = 2f;
    }

    // Drag 시작할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragEvent(eventData);
    }

    // Drag 하는 중일 때
    public void OnDrag(PointerEventData eventData)
    {
        DragEvent(eventData);
    }

    // Drag 끝났을 때
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        lever.anchoredPosition = Vector2.zero;
        GameManager.instance.player.ps = PlayerState.Idle;
    }

    void DragEvent(PointerEventData eventData)
    {
        isDrag = true;
        Vector2 inputPos = eventData.position - GetComponent<RectTransform>().anchoredPosition;
        Vector2 rangeDir = inputPos.magnitude < lever_range ? inputPos : inputPos.normalized * lever_range;
        lever.anchoredPosition = rangeDir;
        input_vector = rangeDir.normalized;
    }

    void PlayerControl(Player p)
    {
        
        if (isDrag)
        {
            if(input_vector.x > 0)
            {
                p.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                p.GetComponent<SpriteRenderer>().flipX = false;
            }
            p.transform.Translate(new Vector2(input_vector.x, input_vector.y) * Time.deltaTime * Speed);
            // Clamp 사용해서 캐릭터 움직임 제한
            Vector3 temp;
            temp = new Vector2(Mathf.Clamp(p.transform.position.x, -10f, 10f), Mathf.Clamp(p.transform.position.y, -9f, 9f));
            p.transform.position = temp;
            // 움직일때 레이저 좌표도 같이 움직임
            GameManager.instance.playerSpawnManager.tmp_laser_parent.transform.position = p.transform.position;
            p.ps = PlayerState.Walk;
        }
    }

    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            PlayerControl(player);
        }
    }
}
