using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Missile : MonoBehaviour
{
    public List<Enemy> findEnemy;
    public Missile_Bullet mi;
    float time = 0;
    //public Enemy e;
    void Start()
    {
        //ObjEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2)
            Init();
        FindEnemy();
        //Find();
        //Find();
    }

    void FindEnemy()
    {
        Enemy[] e = FindObjectsOfType<Enemy>().OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToArray();
        findEnemy.Clear();
        for (int i = 0; i < e.Length; i++)
        {
            findEnemy.Add(e[i]);
            if (findEnemy.Count >= 6)
                break;
        }
    }

    void Init()
    {

        for (int i = 0; i < 6; i++)
        {
            Missile_Bullet missile_Bullet = Instantiate(mi, transform);
            missile_Bullet.transform.SetParent(transform.GetChild(0));
            //return missile_Bullet;
        }
    }
    /*void Find()
    {
        
        for (int i = 0; i < findEnemy.Count; i++)
        {
            Vector2 vec = mi[i].transform.position - findEnemy[i].transform.position;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            mi[i].transform.rotation = rotation;
            mi[i].transform.Translate(Vector3.up * 2 * Time.deltaTime);
        }
    }*/
}
