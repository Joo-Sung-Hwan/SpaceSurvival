using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBomb : Bomb
{
    ParticleSystem particle;
    public Transform targetTrans;
    public float magnetStrength = 5f;
    public float magnetDistance = 10f;
    public float magnetDir = 1f;
    public bool looseMagnet = true;
    public bool magnetBombZone;

    public override void Init()
    {
        bt = BombType.Magnet;
        bd.BombAttack = 2f;
    }

    void Start()
    {
        Init();
        ResetData();
        SetBombAttack(2);
        SetBombSize(1);
    }

    public override void ResetData()
    {
        base.ResetData();
        particle = GetComponent<ParticleSystem>();
    }

    public void MagnetEvents()
    {
        if(magnetBombZone)
        {
            Vector2 dirMagnet = transform.position - targetTrans.transform.position;
            float distance = Vector2.Distance(transform.position, targetTrans.position);
            float magnetDisStr = (magnetDistance / distance) * magnetStrength;
            targetTrans.GetComponent<Rigidbody2D>().AddForce(magnetDisStr * (dirMagnet * magnetDir), ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            targetTrans = collision.transform;
            magnetBombZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && looseMagnet)
        {
            magnetBombZone = false;
        }
    }
}
