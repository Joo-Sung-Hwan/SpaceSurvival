using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollingSystem : MonoBehaviour
{
    Queue<Bullet> b_queue;
    Queue<Laser> l_queue;
    Queue<Enemy> e_queue;
    Queue<Bomb> bo_queue;
    Queue<Item> item_queue;
    
    // Start is called before the first frame update
    void Start()
    {
        b_queue = new Queue<Bullet>();
        l_queue = new Queue<Laser>();
        e_queue = new Queue<Enemy>();
        bo_queue = new Queue<Bomb>();
        item_queue = new Queue<Item>();
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
            return bul;
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

    public Laser PollingLaser(Laser laser, Transform parent)
    {
        Laser l = null;
        if (l_queue.Count == 0)
        {
            Laser la = Instantiate(laser, parent);
            l_queue.Enqueue(la);
            la.gameObject.SetActive(false);
            return la;
        }
        foreach (Laser item in l_queue)
        {
            if (!item.gameObject.activeSelf)
            {
                l = item;
                l.SetData();
                l.transform.position = parent.position;
                l.gameObject.SetActive(true);
                break;
            }
        }
        if (l == null)
        {
            l = Instantiate(laser, parent);
            l_queue.Enqueue(l);
        }
        l.gameObject.SetActive(true);
        return laser;
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
                b.ResetData();
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
        //Debug.Log("생성");
        b.gameObject.SetActive(true);
        return bomb;
    }
    public Enemy PollingEnemy(Enemy enemy, Vector2 parent)
    {
        Enemy e = null;
        if (e_queue.Count == 0)
        {
            Enemy em = Instantiate(enemy, parent, Quaternion.identity);
            em.Init();
            e_queue.Enqueue(em);
            em.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_enemy_parent);
            return em;
        }
        foreach (Enemy item in e_queue)
        {
            if (!item.gameObject.activeSelf)
            {
                e = item;
                e.Init();
                e.IsDead = false;
                e.transform.position = parent;
                e.gameObject.SetActive(true);
                break;
            }
        }
        if (e == null)
        {
            e = Instantiate(enemy, parent, Quaternion.identity);
            e.Init();
            e.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_enemy_parent);
            e_queue.Enqueue(e);
        }
        e.gameObject.SetActive(true);
        return enemy;
    }

    public Item PollingItem(Item item, Transform parent)
    {
        Item i = null;
        if(item_queue.Count == 0)
        {
            Item it = Instantiate(item, parent);
            it.Init();
            item_queue.Enqueue(it);
            it.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_item_parent);
            return it;
        }
        foreach(Item items in item_queue)
        {
            if(!items.gameObject.activeSelf)
            {
                i = items;
                i.Init();
                i.transform.parent = parent;
                i.gameObject.SetActive(true);
                break;
            }
        }
        if(i == null)
        {
            i = Instantiate(item, parent);
            i.Init();
            i.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_item_parent);
            item_queue.Enqueue(i);
        }
        i.gameObject.SetActive(true);
        return i;
    }
}
