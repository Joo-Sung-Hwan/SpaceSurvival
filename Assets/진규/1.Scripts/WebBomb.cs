using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBomb : Bomb
{
    public ParticleSystem particle;
    Transform WebTrans;
    Animator ani;

    public override void Init()
    {
        bt = BombType.Web;
        WebTrans = transform.GetChild(3);
    }

    void Start()
    {
        Init();
        ResetData();
        SetBombAttack(2);
        SetBombSize(1);
        SetBombDebuff(0.25f);
    }

    public override void ResetData()
    {
        base.ResetData();
        ani = GetComponent<Animator>();
        AtiveObj(true);
    }

    public void WebState(BombState bs)
    {
        if (bs == BombState.Idle)
        {
            ani.SetTrigger("idle");
        }
        else
        {
            StartCoroutine(Bomb());
        }
    }

    //��ź ������ ��ƼŬ ȿ��
    public IEnumerator Bomb()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(particle, transform);
        yield return new WaitForSeconds(0.3f);
        AtiveObj(false);
        WebTrans.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(4f);
        WebTrans.GetComponent<CircleCollider2D>().enabled = false;
    }

    void AtiveObj(bool isActive)
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = isActive;
        }
    }
}
