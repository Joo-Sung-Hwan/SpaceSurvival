using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Missile : MonoBehaviour
{
    public List<Enemy> findEnemy;
    public List<Missile_Bullet> missiles;
    public Missile_Bullet mi;
    float time = 0;
    //public Enemy e;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*time += Time.deltaTime;
        if (time > 5)
        {
            time = 0;
            StartCoroutine("Fire");
        }
            //Init();
        //FindEnemy();*/
    }

    void FindEnemy()
    {
        Enemy[] enemy = FindObjectsOfType<Enemy>().OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToArray();
        findEnemy.Clear();
        for (int i = 0; i < enemy.Length; i++)
        {
            findEnemy.Add(enemy[i]);
            if (findEnemy.Count >= 6)
                break;
        }
    }

    IEnumerator Fire()
    {
        FindEnemy();
        yield return new WaitForSeconds(0.5f);
        Init();
        yield return new WaitForSeconds(0.1f);
        mi.DrawTrajectory();
    }
    void Init()
    {
        missiles.Clear();
        for (int i = 0; i < 6; i++)
        {
            Missile_Bullet missile_Bullet = Instantiate(mi, transform);
            missile_Bullet.transform.SetParent(transform.GetChild(0));
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
