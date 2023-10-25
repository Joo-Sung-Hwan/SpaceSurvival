using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBomb : Bomb
{
    public override void Init()
    {
        bt = BombType.Web;
        bd.zoneTrans = transform.GetChild(3);
        bd.firstColor = new Color(1f, 23f / 255f, 206f / 255f, 1f);
        bd.secondColor = new Color(220f / 255f, 146f / 255f, 1f, 1f);
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
        AtiveObj(transform, true);
    }
}
