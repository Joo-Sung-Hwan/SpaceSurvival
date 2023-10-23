using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBomb : Bomb
{
    public ParticleSystem particle;
    public Transform magnetTrans;
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    SpriteRenderer sr;

    public override void Init()
    {
        bt = BombType.Magnet;
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
    }

    void Start()
    {
        Init();
        ResetData();
        SetBombAttack(2);
        SetBombSize(1);
        Paticle();
    }

    public override void ResetData()
    {
        base.ResetData();
        particle = GetComponent<ParticleSystem>();
    }

    public void MagnetState(BombState bs)
    {
        if(bs == BombState.Idle)
        {
        }
        else
        {
            Debug.Log(transform.GetChild(4));

            StartCoroutine(Bomb());
            Debug.Log(StartCoroutine(Bomb()));
        }
    }

    void Paticle()
    {
        if (particles != null)
            return;
        particles.Add(particle);
        for(int i = 0; i < 4; i++)
        {
            particles.Add(transform.GetChild(i).GetComponent<ParticleSystem>());
        }
    }

    public IEnumerator Bomb()
    {
        yield return new WaitForSeconds(0.5f);
        sr.enabled = false;
        yield return new WaitForSeconds(1f);
        magnetBombZone = true;
        foreach (var par in particles)
            par.startDelay = 1.5f;
        transform.GetChild(4).GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(1f);
        transform.GetChild(4).GetComponent<Collider2D>().enabled = false;
        magnetBombZone = false;
    }
}
