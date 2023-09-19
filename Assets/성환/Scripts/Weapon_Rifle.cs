using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Rifle : MonoBehaviour
{
    public Bullet[] bullets;
    public Laser[] laser;
    public Transform bullet_parent;
    float fire_time = 1f;
    float time_D = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Firelaser();
    }
    public void FireBullet()
    {
        time_D += Time.deltaTime;
        if (time_D > fire_time)
        {
            GameManager.instance.pollingsystem.PollingBullet(bullets[0], bullet_parent);
            time_D = 0f;
        }
    }

    public void Firelaser()
    {
        if(bullet_parent.childCount == 1)
        {
            return;
        }
        Instantiate(bullets[0], bullet_parent);
    }
}
