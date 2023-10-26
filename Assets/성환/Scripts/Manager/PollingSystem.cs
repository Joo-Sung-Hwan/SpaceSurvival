using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PollingSystem : MonoBehaviour
{
    Queue<Bullet> b_queue;
    Queue<Laser> l_queue;
    Queue<Enemy> e_queue;
    Queue<Bomb> bo_queue;
    Queue<Item> item_queue;
    Queue<SelectCard> s_queue;
    Queue<DamageTxt> t_queue;
    
    // Start is called before the first frame update
    void Start()
    {
        b_queue = new Queue<Bullet>();
        l_queue = new Queue<Laser>();
        e_queue = new Queue<Enemy>();
        bo_queue = new Queue<Bomb>();
        item_queue = new Queue<Item>();
        s_queue = new Queue<SelectCard>();
        t_queue = new();
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
            bul.init();
            bul.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_bullet_parent);
            return bul;
        }
        foreach (Bullet item in b_queue)
        {
            if (!item.gameObject.activeSelf)
            {
                b = item;
                b.init();
                b.transform.position = parent.position;
                b.gameObject.SetActive(true);
                break;
            }
        }
        if(b == null)
        {
            b = Instantiate(bullet, parent);
            b.init();
            b.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_bullet_parent);
            b_queue.Enqueue(b);
        }
        
        b.gameObject.SetActive(true);
        return b;
    }

    public Laser PollingLaser(Laser laser, Transform parent)
    {
        Laser l = null;
        if (l_queue.Count == 0)
        {
            Laser la = Instantiate(laser, parent);
            l_queue.Enqueue(la);
            la.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_laser_parent);
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
            l.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_laser_parent);
            l_queue.Enqueue(l);
        }
        l.gameObject.SetActive(true);
        return l;
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
                //b.Init();
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
        return b;
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
                e.IsDead = false;
                e.Init();
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
        return e;
    }

    public Item PollingItem(Item item, Transform parent)
    {
        Item i = null;
        if (item_queue.Count == 0)
        {
            Item it = Instantiate(item, parent);
            it.Init();
            item_queue.Enqueue(it);
            it.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_item_parent);
            return it;
        }
        foreach (Item items in item_queue)
        {
            if (!items.gameObject.activeSelf)
            {
                i = items;
                i.Init();
                i.transform.position = parent.position;
                i.gameObject.SetActive(true);
                break;
            }
        }
        if (i == null)
        {
            i = Instantiate(item, parent);
            i.Init();
            i.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_item_parent);
            item_queue.Enqueue(i);
        }
        i.gameObject.SetActive(true);
        return i;
    }
    
    public SelectCard PoolingSelectCard(SelectCard card, Transform parent)
    {
        SelectCard s = null;
        if (s_queue.Count == 0)
        {
            SelectCard sc = Instantiate(card, parent);
            s_queue.Enqueue(sc);
            sc.Init();
            sc.transform.SetParent(GameManager.instance.selectCardManager.selectcard_parent);
            return sc;
        }
        foreach (SelectCard item in s_queue)
        {
            if (!item.gameObject.activeSelf)
            {
                s = item;
                s.Init();
                s.transform.position = parent.position;
                s.gameObject.SetActive(true);
                return s;
            }
        }
        if (s == null)
        {
            s = Instantiate(card, parent);
            s.Init();
            s.transform.SetParent(GameManager.instance.selectCardManager.selectcard_parent);
            s_queue.Enqueue(s);
        }
        //Debug.Log("생성");
        s.gameObject.SetActive(true);
        return s;
    }
    
    public DamageTxt PoolingDamageTxt(DamageTxt text, Vector3 pos, Canvas canvas, float damage)
    {
        DamageTxt damageT = null;
        if(t_queue.Count == 0)
        {
            DamageTxt dt = Instantiate(text, pos, Quaternion.identity, canvas.transform);
            t_queue.Enqueue(dt);
            //dt.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_damageText_Parent);
            text.GetComponent<TMP_Text>().text = damage.ToString();
        }
        foreach(DamageTxt damageTxt in t_queue)
        {
            if(!damageTxt.gameObject.activeSelf)
            {
                damageT = damageTxt;
                damageT.transform.position = canvas.transform.position;
                damageT.gameObject.SetActive(true);
                return damageT;
            }
        }
        if(damageT == null)
        {
            damageT = Instantiate(text, pos, Quaternion.identity, canvas.transform);
            t_queue.Enqueue(damageT);
            //damageT.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_damageText_Parent);
            text.GetComponent<TMP_Text>().text = damage.ToString();
        }
        damageT.gameObject.SetActive(true);
        return damageT;
    }
}
