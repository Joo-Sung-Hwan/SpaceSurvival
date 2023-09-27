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
    float desTime = 0f;
    Vector3 start_pos;
    float dis;
    Vector2 destination;
    float maxdis;
    float gravity = 10f;
    [HideInInspector] public float BombAttack { get; set; }
    Vector2 dir;
    
    bool isGrounded = true;

    float maxheight;
    float curheight;

    public Transform sprite;
    public Transform shadow;

    public GameObject explosion;
    Player player;


    Animator ani;
    BombState bs;

    

    void OnEnable()
    {
        SetBombAttack(50);
        //Debug.Log(BombAttack);
        sprite.gameObject.SetActive(true);
        shadow.gameObject.SetActive(true);
        bs = BombState.Idle;
        ani = GetComponent<Animator>();
        B_State(bs);
        player = GameManager.instance.playerSpawnManager.player;
        start_pos = player.area.bounds.center;
        destination = GameManager.instance.GetRandomPosition(player.transform,player.area);
        maxdis = Vector3.Distance(player.area.bounds.center, destination);
        //Debug.Log(player.area.bounds.center);
        curheight = 1f;
        maxheight = curheight;
        Init(destination);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded)
        {
            curheight += -gravity * Time.deltaTime;
            sprite.position += new Vector3(0, curheight, 0) * Time.deltaTime;
            transform.position += (Vector3)dir * Time.deltaTime;

            CheckGroundHit();
        }
    }


    public void DestroyBomb()
    {
        desTime += Time.deltaTime;
        if(desTime > 10f)
        {
            gameObject.SetActive(false);
            desTime = 0f;
        }
    } 
    public void Init(Vector2 dir)
    {
        isGrounded = false;
        maxheight = 1.5f;
        this.dir = dir / 1.2f;
        curheight = maxheight;
    }

    public void CheckGroundHit()
    {
        dis = Vector3.Distance(this.transform.position, start_pos);
        if(dis < maxdis)
        {
            if (sprite.position.y < shadow.position.y)
            {
                sprite.position = shadow.position;
                Init(dir);
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
            sprite.gameObject.SetActive(false);
            shadow.gameObject.SetActive(false);
            explosion.SetActive(true);
            GetComponent<Animator>().Play("BombExplosion");
        }
    }

    public void ExplosionFinish()
    {
        explosion.SetActive(false);
        bs = BombState.Idle;
        gameObject.SetActive(false);
    }

    public void SetBombAttack(float bombattack)
    {
        BombAttack = bombattack;
    }
}
