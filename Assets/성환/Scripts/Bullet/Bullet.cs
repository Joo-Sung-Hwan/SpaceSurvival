using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Speed { get; set; }
    protected float destroy_time = 0f;
    bool dir;
    public abstract void init();

    public virtual void Move()
    {
        if (dir)
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * Speed;
        }
    }

    public virtual void SetDir()
    {
        if (GameManager.instance.playerSpawnManager.player.GetComponent<Transform>().localScale.x > 0)
        {
            dir = true;
        }
        else
        {
            dir = false;
        }
    }

    public virtual void DestroyBullet()
    {
        destroy_time += Time.deltaTime;
        if(destroy_time > 3f)
        {
            gameObject.SetActive(false);
            destroy_time = 0f;
        }
    }
}
