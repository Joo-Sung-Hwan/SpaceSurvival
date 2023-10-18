using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum BombState
{
    Idle,
    Explosion
}
public class Bomb : MonoBehaviour
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
    float curheight;

    [HideInInspector] public float BombAttack { get; set; }
    [HideInInspector] public float BombSize { get; set; }

    public Transform sprite;
    public Transform shadow;

    public GameObject explosion;
    Player player;

    Animator ani;
    BombState bs;

    void Start()
    {
        ResetData();
        SetBombAttack(50);
        SetBombSize(1);
    }
    public void ResetData()
    {
        SetBombAttack(BombAttack);
        SetBombSize(BombSize);
        curbounce = 0;
        curheight = 1f;
        SettingActive(true);
        ani = GetComponent<Animator>();
        player = GameManager.instance.player;
        start_pos = player.area.bounds.center;
        destination = GameManager.instance.GetRandomPosition(player.transform, player.area);
        maxdis = Vector3.Distance(start_pos, destination);
        Init(destination);
        bs = BombState.Idle;
        B_State(bs);
    }

    // Update is called once per frame
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
    
    public void Init(Vector2 dir)
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
                Init(dir / 1.5f);
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

    // 폭탄 터지는 애니메이션
    public void B_State(BombState bs)
    {
        if(bs == BombState.Idle)
        {
            ani.SetTrigger("idle");
            ani.ResetTrigger("explosion");
            explosion.SetActive(false);
        }
        else
        {
            ani.ResetTrigger("idle");
            ani.SetTrigger("explosion");
            SettingActive(false);
            explosion.SetActive(true);
            GetComponent<Animator>().Play("BombExplosion");
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
        BombAttack = bombattack;
    }

    // 폭탄 터지는 범위 설정
    public void SetBombSize(float size)
    {
        BombSize = size;
        GetComponent<Transform>().localScale = new Vector3(BombSize, BombSize, 0f);
    }
}
