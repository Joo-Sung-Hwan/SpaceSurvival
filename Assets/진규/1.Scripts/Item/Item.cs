using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemInfo
{
    public int exp;
}
public abstract class Item : MonoBehaviour
{
    public ItemInfo info = new ItemInfo();

    public float magnetStrength = 5f;
    public float distance = 10f;
    public float magnetDir = 1f;
    public bool looseMaget = true;
    public bool magnetZone;

    Rigidbody2D rigid;
    Transform magnetTrans;
    float time = 0;
    public abstract void Init();

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if(time > 5f)
        {
            time = 0;
            gameObject.SetActive(false);
        }
        Magnet();
    }

    public virtual void Magnet()
    {
        if (magnetZone)
        {
            Vector2 dirMagnet = magnetTrans.position - transform.position;
            float distance = Vector2.Distance(magnetTrans.position, transform.position);
            float magnetDisStr = (this.distance / distance) * magnetStrength;
            rigid.AddForce(magnetDisStr * (dirMagnet * magnetDir), ForceMode2D.Force);
        }
    }

    // 플레이어와 Trigger 적용
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pet pet = collision.GetComponent<Pet>();
        if(collision.CompareTag("Player") && pet == null)
        {
            GameManager.instance.player.definePD.CurExp += info.exp;
            gameObject.SetActive(false);
        }
        if(collision.CompareTag("Magnet"))
        {
            magnetTrans = collision.transform;
            magnetZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Magnet") && looseMaget)
        {
            magnetZone = false;
        }
    }
}
