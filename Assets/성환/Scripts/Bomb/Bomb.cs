using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BombState
{
    Idle,
    Explosion
}

public enum BombType
{
    Nomal,
    Magnet,
    Web,
    Fire
}

public struct BombTypeClass
{
    public NormalBomb nomalBomb;
    public MagnetBomb magnetBomb;
    public WebBomb webBomb;
    public FireBomb fireBomb;
}

public struct BombData
{
    public float BombAttack { get; set; }
    public float BombSize { get; set; }
    public float BombDebuff { get; set; }
    public Transform zoneTrans;
    public Color firstColor;
    public Color secondColor;
}

public abstract class Bomb : MonoBehaviour
{
    Vector3 start_pos;
    Vector2 dir;
    Vector2 destination;
    float dis;
    float maxdis;
    float gravity = 10f;
    bool isGrounded = true;
    int maxbounce = 5;
    int curbounce;

    public BombTypeClass btc = new();
    public BombData bd = new();

    public Transform sprite;
    public Transform shadow;
    public ParticleSystem particle;
    [HideInInspector]public Animator ani;
    float curheight;

    public GameObject explosion;
    Player player;

    protected BombState bs;
    public BombType bt;

    public abstract void Init();

    public virtual void ResetData()
    {
        SetBombAttack(bd.BombAttack);
        SetBombSize(bd.BombSize);
        curbounce = 0;
        
        SettingActive(true);
        player = GameManager.instance.player;
        start_pos = player.area.bounds.center;
        destination = GameManager.instance.GetRandomPosition(player.transform, player.area);
        maxdis = Vector3.Distance(start_pos, destination);
        DirInit(destination);
        bs = BombState.Idle;
    }

    void Update()
    {
        if (!isGrounded)
        {
            curheight += -gravity * Time.deltaTime;

            // 폭탄 이미지 위아래로 바운드
            sprite.position += new Vector3(0, curheight, 0) * Time.deltaTime;
            transform.position += (Vector3)dir * Time.deltaTime;

            CheckGroundHit();
        }
    }

    void DirInit(Vector2 dir)
    {
        isGrounded = false;
        this.dir = dir;
        curheight = 1.5f;
        curbounce++;
    }

    public void CheckGroundHit()
    {
        dis = Vector3.Distance(this.transform.position, start_pos);

        // 최대거리이거나 3번 바운드하면 터짐
        if(dis < maxdis && curbounce < maxbounce)
        {
            if (sprite.position.y < shadow.position.y)
            {
                sprite.position = shadow.position;
                DirInit(dir / 1.5f);
            }
        }
        else
        {
            bs = BombState.Explosion;
            Vector3 tmp = shadow.position;
            tmp.y = tmp.y + 0.04f;
            sprite.position = tmp;
            isGrounded = true;
            B_State(bs);
        }
    }

    // 폭탄 터지는 애니메이션 끝났을 때 호출
    public void ExplosionFinish()
    {
        explosion.SetActive(false);
        bs = BombState.Idle;
        gameObject.SetActive(false);
    }

    public void SettingActive(bool active)
    {
        sprite.gameObject.SetActive(active);
        shadow.gameObject.SetActive(active);
    }

    // 폭탄 공격력 설정
    public void SetBombAttack(float bombattack)
    {
        bd.BombAttack = bombattack;
    }

    // 폭탄 터지는 범위 설정
    public void SetBombSize(float size)
    {
        bd.BombSize = size;
        GetComponent<Transform>().localScale = new Vector3(bd.BombSize, bd.BombSize, 0f);
    }

    public void SetBombDebuff(float bombDebuff)
    {
        bd.BombDebuff = bombDebuff;
    }

    //폭탄 터지는 파티클 효과
    public IEnumerator BombParticle(float delay)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(particle, transform);
        AtiveObj(gameObject.transform,false);
        bd.zoneTrans.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(delay);
        bd.zoneTrans.GetComponent<CircleCollider2D>().enabled = false;
    }

    // 폭탄 터지는 이벤트 구현
    public void B_State(BombState bs)
    {
        if (bs == BombState.Idle)
        {
            ani.SetTrigger("idle");
        }
        else if(particle == null)
        {
            ani.ResetTrigger("idle");
            ani.SetTrigger("explosion");
            SettingActive(false);
            explosion.SetActive(true);
            GetComponent<Animator>().Play("BombExplosion");
        }
        else if(particle != null)
        {
            StartCoroutine(BombParticle(BombEvents()));
        }
    }

    public void AtiveObj(Transform trans,bool isActive)
    {
        for (int i = 0; i < 2; i++)
        {
            trans.GetChild(i).GetComponent<SpriteRenderer>().enabled = isActive;
        }
    }

    // 폭탄타입에 따른 이벤트 구현
    float BombEvents()
    {
        float delay = 0;
        switch(GameManager.instance.player.bomb.bt)
        {
            case BombType.Magnet:
                delay = 0.2f;
                return delay;
            case BombType.Web:
                delay = 4f;
                return delay;
            case BombType.Fire:
                delay = 2f;
                return delay;
        }
        return delay;
    }
}
