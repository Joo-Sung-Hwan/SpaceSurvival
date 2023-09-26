using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollingSystem : MonoBehaviour
{
    Queue<Bullet> b_queue;
    Queue<Bullet> l_queue;
    Queue<Enemy> e_queue;
    Queue<Bomb> bo_queue;
    
    // Start is called before the first frame update
    void Start()
    {
        b_queue = new Queue<Bullet>();
        l_queue = new Queue<Bullet>();
        e_queue = new Queue<Enemy>();
        bo_queue = new Queue<Bomb>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 총알 오브젝트 풀링
    public Bullet PollingBullet(Bullet bullet, Transform parent)
    {
        Bullet b = null;
        if (b_queue.Count == 0)
        {
            Bullet bul = Instantiate(bullet, parent);
            b_queue.Enqueue(bul);
            bul.gameObject.SetActive(false);
            bul.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_bullet_parent);
        }
        foreach (Bullet item in b_queue)
        {
            if (!item.gameObject.activeSelf)
            {
                b = item;
                b.transform.position = parent.position;
                b.gameObject.SetActive(true);
                break;
            }
        }
        if(b == null)
        {
            b = Instantiate(bullet, parent);
            b.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_bullet_parent);
            b_queue.Enqueue(b);
        }
        
        b.gameObject.SetActive(true);
        return bullet;
    }

    public Bullet PollingLaser(Bullet bullet, Transform parent)
    {
        Bullet l = null;
        if (l_queue.Count == 0)
        {
            Bullet bul = Instantiate(bullet, parent);
            l_queue.Enqueue(bul);
            bul.gameObject.SetActive(false);
        }
        foreach (Bullet item in l_queue)
        {
            if (!item.gameObject.activeSelf)
            {
                l = item;
                l.transform.position = parent.position;
                l.gameObject.SetActive(true);
                break;
            }
        }
        if (l == null)
        {
            l = Instantiate(bullet, parent);
            l_queue.Enqueue(l);
        }
        l.gameObject.SetActive(true);
        return bullet;
    }
    public Bomb PollingBomb(Bomb bomb, Transform parent)
    {
        Bomb b = null;
        if (bo_queue.Count == 0)
        {
            Bomb bul = Instantiate(bomb, parent);
            bo_queue.Enqueue(bul);
            bul.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_bomb_parent);
            return bul;
        }
        foreach (Bomb item in bo_queue)
        {
            if (!item.gameObject.activeSelf)
            {
                b = item;
                b.transform.position = parent.position;
                b.gameObject.SetActive(true);
                break;
            }
        }
        if (b == null)
        {
            b = Instantiate(bomb, parent);
            b.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_bomb_parent);
            bo_queue.Enqueue(b);
        }
        Debug.Log("생성");
        b.gameObject.SetActive(true);
        return bomb;
    }
    public Enemy PollingEnemy(Enemy enemy, Transform parent)
    {
        Enemy e = null;
        if (e_queue.Count == 0)
        {
            Enemy em = Instantiate(enemy, parent);
            e_queue.Enqueue(em);
            em.gameObject.SetActive(false);
            em.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_enemy_parent);
        }
        foreach (Enemy item in e_queue)
        {
            if (!item.gameObject.activeSelf)
            {
                e = item;
                e.transform.position = parent.position;
                e.gameObject.SetActive(true);
                break;
            }
        }
        if (e == null)
        {
            e = Instantiate(enemy, parent);
            e.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_enemy_parent);
            e_queue.Enqueue(e);
        }
        e.gameObject.SetActive(true);
        return enemy;
    }
}
