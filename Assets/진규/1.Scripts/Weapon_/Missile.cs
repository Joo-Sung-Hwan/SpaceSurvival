using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Missile : MonoBehaviour
{
    public List<Enemy> findEnemy;
    public List<Weapon> missiles;
    public Weapon mi;
    [Space(10f)]
    int count = 12;
    [Space(10f)]
    public float disStart = 3.0f;
    public float disEnd = 1.0f;
    [Range(0, 1)] public float m_interval = 0.15f;
    public int countInterval = 2;
    float time = 0;
    float destroyTime = 0;
    //public Enemy e;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //FireBullet();
        //DestroyBullet();
        //Init();
        //FindEnemy();
    }
    public Transform FindE()
    {
        Enemy enemy = FindObjectOfType<Enemy>();

        return enemy.transform;
    }
    public Enemy[] FindEnemy()
    {
        Enemy[] enemy = FindObjectsOfType<Enemy>().OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToArray();
        findEnemy.Clear();
        for (int i = 0; i < enemy.Length; i++)
        {
            findEnemy.Add(enemy[i]);
            if (findEnemy.Count >= 6)
                break;
        }
        return findEnemy.ToArray();
    }
    void FireBullet()
    {
        time += Time.deltaTime;
        if(time > 5)
        {
            time = 0;
            StartCoroutine("Fire");
        }
    }
    void DestroyBullet()
    {
        destroyTime += Time.deltaTime;
        if (destroyTime > 8)
        {
            destroyTime = 0;
            ObjectPoolSystem.ObjectPoolling<Weapon>.ReturnPool(mi, ObjectName.Missile);
        }
    }
    IEnumerator Fire()
    {
        //yield return new WaitForSeconds(1f);
        int _shot = count;
        while(_shot > 0)
        {
            FindEnemy();

            for (int i = 0; i < countInterval; i++)
            {
                if (_shot > 0)
                {
                    Weapon missile = ObjectPoolSystem.ObjectPoolling<Weapon>.GetPool(mi, ObjectName.Missile, this.transform);
                    missile.Initalize();
                    missile.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_missile_parent);
                    missile.GetComponent<Missile_Bullet>().Init(transform, findEnemy[0].transform, 0.5f, disStart, disEnd);
                    //missile.GetComponent<Missile_Bullet>().master = transform.gameObject;
                    //missile.GetComponent<Missile_Bullet>().enemy = findEnemy[0].gameObject;
                    _shot--;
                }
            }
            yield return new WaitForSeconds(m_interval);
        }
        yield return null;
    }
    void Init()
    {
        missiles.Clear();
        for (int i = 0; i < findEnemy.Count; i++)
        {
            Weapon missile_Bullet = ObjectPoolSystem.ObjectPoolling<Weapon>.GetPool(mi,ObjectName.Missile, transform);
            missile_Bullet.Initalize();
            missile_Bullet.transform.SetParent(transform.GetChild(0));
            //missile_Bullet.GetComponent<Missile_Bullet>().master = transform.gameObject;
            //missile_Bullet.GetComponent<Missile_Bullet>().enemy = findEnemy[i].gameObject;
            missiles.Add(missile_Bullet);
        }
    }
    void Test()
    {
        for(int i = 0; i < findEnemy.Count; i++)
        {
            Vector2 vec = missiles[i].transform.position - findEnemy[i].transform.position;
            Vector3 axis = Vector3.Cross(vec, missiles[i].transform.forward);
            Quaternion.AngleAxis(90, axis);
            Quaternion rot = Quaternion.AngleAxis(Time.deltaTime * 45, axis);
            missiles[i].transform.rotation = Quaternion.Lerp(missiles[i].transform.rotation, rot, 50 * Time.deltaTime);
            missiles[i].transform.Translate(Vector3.up * 2 * Time.deltaTime);
        }
    }
    void Find()
    {
        for (int i = 0; i < findEnemy.Count; i++)
        {
            Vector2 vec = missiles[i].transform.position - findEnemy[i].transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            missiles[i].transform.rotation = rotation;
            missiles[i].transform.Translate(Vector3.up * 2 * Time.deltaTime);
        }
    }
}
