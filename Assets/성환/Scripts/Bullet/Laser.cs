using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public int ReflectMaxCount { get; set; }
    int reflectcount;
    Vector2 nextDir;
    float roZ;
    RaycastHit2D ray;
    public LayerMask layermask;
    LineRenderer lr;
    public void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = Color.blue;
        lr.endColor = Color.blue;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        ReflectMaxCount = 5;
        SetData();
        
    }

    void Update()
    {
       // Debug.DrawRay(transform.position, nextDir, Color.blue);
        transform.position += (Vector3)nextDir * Time.deltaTime * 3f;
        DestroyLaser();
    }
    public void DestroyLaser()
    {
        if(reflectcount >= ReflectMaxCount)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void SetData()
    {
        int rand = Random.Range(0, 4);
        reflectcount = 0;
        nextDir = GameManager.instance.GetRandomPosition(GameManager.instance.spawnManager.spawnPoint[rand].transform, GameManager.instance.spawnManager.spawnPoint[rand]).normalized;
        roZ = GetAngle(GameManager.instance.playerSpawnManager.player.transform.position, nextDir);
        transform.rotation = Quaternion.Euler(0, 0, roZ);
        ShootRay();
    }
    
    public void ShootRay()
    {
        Vector2 newposition = transform.position; ;
        Vector2 newDir = nextDir;
        
        lr.positionCount = ReflectMaxCount;
        lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1f));
        for(int i = 1; i < lr.positionCount; i++)
        {
            ray = Physics2D.Raycast(newposition, newDir, 100f, layermask);
            lr.SetPosition(i, new Vector3(ray.point.x, ray.point.y, -1f));
            newposition = ray.point;
            newDir = Vector2.Reflect(newDir, ray.normal);
        }
    }
    


    public float GetAngle(Vector2 b_dir, Vector2 a_dir)
    {
        Vector2 v = a_dir - b_dir;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

   
    
}
