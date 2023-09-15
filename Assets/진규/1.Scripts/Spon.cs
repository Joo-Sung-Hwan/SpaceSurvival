using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spon : MonoBehaviour
{
    public Enemy obj;
    public Transform trans;
    void Start()
    {
        StartCoroutine(MonsterSpawn());
        //enemy.DataInfo(obj.name);
    }

    void Update()
    {
        
    }

    IEnumerator MonsterSpawn()
    {
        yield return new WaitForSeconds(4f);
        Enemy enemy = Instantiate(obj, trans);
        enemy.Init();
    }
}
