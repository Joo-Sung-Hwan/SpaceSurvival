using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Missile : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindEnemy()
    {
        List<Enemy> enemies = FindObjectsOfType<Enemy>().ToList();

        for(int i = 0; i < 10; i++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            foreach(var enemy in enemies)
            {
                float dis = Vector3.Distance(transform.position, enemy.transform.position);
            }
        }
    }
}
