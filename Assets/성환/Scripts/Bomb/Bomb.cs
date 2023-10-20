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
    Magnet
}

public struct BombData
{
    public float BombAttack { get; set; }
    public float BombSize { get; set; }
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

    public BombData bd = new();

    public Transform sprite;
    public Transform shadow;
    float curheight;

    public GameObject explosion;
    Player player;

    //public Animator ani;
    //public ParticleSystem particle;
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
    }

    private void Update()
    {
        if (!isGrounded)
        {
            curheight += -gravity * Time.deltaTime;

            // ��ź �̹��� ���Ʒ��� �ٿ��
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

        // �ִ�Ÿ��̰ų� 3�� �ٿ���ϸ� ����
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
            BombEvents();
        }
    }

    void BombEvents()
    {
        switch(bt)
        {
            case BombType.Nomal:
                NormalBomb normalBomb = GetComponent<NormalBomb>();
                normalBomb.B_State(bs);
                break;
            case BombType.Magnet:
                MagnetBomb magnetBomb = GetComponent<MagnetBomb>();
                magnetBomb.MagnetEvents();
                break;
        }
    }

    // ��ź ������ �ִϸ��̼� ������ �� ȣ��
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

    // ��ź ���ݷ� ����
    public void SetBombAttack(float bombattack)
    {
        bd.BombAttack = bombattack;
    }

    // ��ź ������ ���� ����
    public void SetBombSize(float size)
    {
        bd.BombSize = size;
        GetComponent<Transform>().localScale = new Vector3(bd.BombSize, bd.BombSize, 0f);
    }
}
