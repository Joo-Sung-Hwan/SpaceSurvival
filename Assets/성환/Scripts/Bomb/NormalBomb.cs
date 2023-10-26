using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBomb : Bomb
{
    public override void Init()
    {
        bt = BombType.Nomal;
        //bd.BombAttack = 10f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        ResetData();
        SetBombAttack(50);
        SetBombSize(1);
    }


    public override void ResetData()
    {
        base.ResetData();
        ani = GetComponent<Animator>();
    }
}
