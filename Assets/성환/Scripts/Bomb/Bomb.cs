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
    Normal,
    Magnet,
    Web,
    Fire
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
    Vector3 destination;
    float dis;
    float maxdis;
    float gravity = 10f;
    bool isGrounded = true;
    int maxbounce = 5;
    protected int curbounce;
    public BombData bd = new();
    public List<Color> colors = new();
    public BombType bt = new();
    public Transform sprite;
    public Transform shadow;
    public ParticleSystem particle;
    [HideInInspector]public Animator ani;
    float curheight;

    Player player;

    protected BombState bs;

    public void Init()
    {
        curbounce = 0;
        SettingActive(true);
        player = GameManager.instance.player;
        destination = GameManager.instance.GetRandomPosition(player.transform, player.area);
        start_pos = player.area.transform.localPosition;
        maxdis = Vector3.Distance(player.transform.position, destination);
        DirInit(destination);
        TransFormObj(transform, true);
        bs = BombState.Idle;
    }
    public override void Attack()
    {
        if (!isGrounded)
        {
            curheight += -gravity * Time.deltaTime;

            // ��ź �̹��� ���Ʒ��� �ٿ��
            sprite.position += new Vector3(0, curheight, 0) * Time.deltaTime;
            transform.position += (Vector3)dir.normalized * Time.deltaTime;

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

    void CheckGroundHit()
    {
        //dis = Vector3.Distance(transform.localPosition, start_pos);
        // �ִ�Ÿ��̰ų� 3�� �ٿ���ϸ� ����
        if(curbounce < maxbounce)
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

    // ��ź ������ �ִϸ��̼� ������ �� ȣ��
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

    //��ź ������ ��ƼŬ ȿ��
    public IEnumerator BombParticle(float delay)
    {
        yield return new WaitForSeconds(0.5f);
        ParticleSystem part = ObjectPoolSystem.ObjectPoolling<ParticleSystem>.GetPool(particle, ObjectName.Particle, transform);
        TransFormObj(gameObject.transform,false);
        weaponData.collider2D.enabled = true;
        yield return new WaitForSeconds(delay);
        weaponData.collider2D.enabled = false;
        ObjectPoolSystem.ObjectPoolling<ParticleSystem>.ReturnPool(part, ObjectName.Particle);
        ObjectPoolSystem.ObjectPoolling<Weapon>.ReturnPool(this, ObjectName.Bomb);
    }

    // ��ź ������ �̺�Ʈ ����
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

    // ��ź ���� collider2D On/Off
    public void TransFormObj(Transform trans,bool isActive)
    {
        for (int i = 0; i < 2; i++)
        {
            trans.GetChild(i).GetComponent<SpriteRenderer>().enabled = isActive;
        }
    }

    // ��źŸ�Կ� ���� �̺�Ʈ ����
    float BombEvents()
    {
        float delay = 0;
        switch(GameManager.instance.player.bomb.GetComponent<Bomb>().bt)
        {
            case BombType.Normal:
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
