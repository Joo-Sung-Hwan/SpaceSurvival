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
    Vector2 input_vector;

    // Player 속성 값
    Player player;
    bool isDrag;

    // Player 움직이는 속도
    [HideInInspector] public float Speed { get; set; }

    void Start()
    {
        player = GameManager.instance.playerSpawnManager.player;
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
        GameManager.instance.playerSpawnManager.player.ps = PlayerState.Idle;
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
            p.ps = PlayerState.Walk;
        }
    }

    void Update()
    {
        PlayerControl(player);
    }
}
