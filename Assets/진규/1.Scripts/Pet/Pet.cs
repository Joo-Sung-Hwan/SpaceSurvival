using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PetData
{
    public float speed;
    public float attack;
    public float buff;
    public float size;
    public CircleCollider2D collider2D;
}

public abstract class Pet : MonoBehaviour
{
    public PetData petData = new();
    public Animator anim;
    public bool MagnetPet { get; set; }
    public bool AttackPet { get; set; }
    protected Player player;

    public abstract void Init();

    void Update()
    {
        player = GameManager.instance.player;
        Move();
    }

    // 플레이어를 적당한 거리선에서 따라다니기
    void Move()
    {
        Vector3 dir = player.transform.position - transform.position;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (dir.normalized.x > 0)
            transform.GetComponent<SpriteRenderer>().flipX = true;
        else if(dir.normalized.x < 0)
            transform.GetComponent<SpriteRenderer>().flipX = false;
        if (distance > 0.5f)
        {
            transform.Translate(dir.normalized * Time.deltaTime * petData.speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exp"))
        {
            Item item = collision.GetComponent<Item>();
            GameManager.instance.player.definePD.CurExp += item.info.exp;
            collision.gameObject.SetActive(false);
        }
    }
}
