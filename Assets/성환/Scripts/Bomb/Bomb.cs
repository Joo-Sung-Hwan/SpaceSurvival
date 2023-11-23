using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BombState
{
    Idle,
    Explosion
}

public struct BombData
{
    public float BombAttack { get; set; }
    //public float bomb_attack;
    public float BombRange { get; set; }
    public float BombDebuff { get; set; }
    public Transform zoneTrans;
    public CircleCollider2D collider2D;
}

public abstract class Bomb : Weapon
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
    public BombData bd = new();
    public List<Color> colors = new();

    public Transform sprite;
    public Transform shadow;
    public ParticleSystem particle;
    [HideInInspector]public Animator ani;
    float curheight;

    Player player;

    protected BombState bs;

    public override void Initalize()
    {
        curbounce = 0;

        SettingActive(true);
        player = GameManager.instance.player;
        start_pos = player.area.bounds.center;
        destination = GameManager.instance.GetRandomPosition(player.transform, player.area);
        maxdis = Vector3.Distance(start_pos, destination);
        DirInit(destination);
        bs = BombState.Idle;
        AtiveObj(transform, true);
    }

    public override void Attack()
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
        bs = BombState.Idle;
        gameObject.SetActive(false);
    }

    public void SettingActive(bool active)
    {
        sprite.gameObject.SetActive(active);
        shadow.gameObject.SetActive(active);
    }

    //폭탄 터지는 파티클 효과
    public IEnumerator BombParticle(float delay)
    {
        yield return new WaitForSeconds(0.5f);
        ParticleSystem part = Instantiate(particle, transform);
        AtiveObj(gameObject.transform,false);
        bd.collider2D.enabled = true;
        yield return new WaitForSeconds(delay);
        bd.collider2D.enabled = false;
        Destroy(part.gameObject);
        Debug.Log(this.name);
        ObjectPoolSystem.ObjectPoolling<Weapon>.ReturnPool(this, 1);
        gameObject.SetActive(false);
    }

    // 폭탄 터지는 이벤트 구현
    public void B_State(BombState bs)
    {
        if (bs == BombState.Idle)
        {
            ani.SetTrigger("idle");
        }
        else
        {
            StartCoroutine(BombParticle(BombEvents()));
        }
    }

    // 폭탄 범위 collider2D On/Off
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
        switch(GameManager.instance.player.weapon.bt)
        {
            case BombType.Nomal:
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
