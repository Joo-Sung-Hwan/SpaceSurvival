using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Speed { get; set; }
    float destroy_time = 10f;
    public Player player;
    public abstract void init();

    public virtual void Move()
    {
        player = GameManager.instance.playerSpawnManager.player;
        // 총알 움직인다.
        /*
        if (player.GetComponent<Transform>().localScale.x > 0f)
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;

        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * Speed;

        }
        */
    }

    public virtual void DestroyBullet()
    {
        float timeD = 0f;
        timeD += Time.deltaTime;
        if(timeD > destroy_time)
        {
            Destroy(this.gameObject);
            timeD = 0f;
        }
    }

    void Update()
    {
        Move();   
        DestroyBullet();
    }
}
