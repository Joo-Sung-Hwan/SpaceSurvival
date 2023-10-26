using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Speed { get; set; }
    public float Attack { get; set; }
    float destroy_time = 0f;
    protected bool dir;
    public abstract void init();

    public void Move()
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

    public void SetDir()
    {
        if (GameManager.instance.player.GetComponent<SpriteRenderer>().flipX)
        {
            dir = false;
        }
        else
        {
            dir = true;
        }
    }

    public void DestroyBullet()
    {
        destroy_time += Time.deltaTime;
        if(destroy_time > 2f)
        {
            gameObject.SetActive(false);
            destroy_time = 0f;
        }
    }
    
    public void FixedUpdate()
    {
        Move();
        DestroyBullet();
    }
}
