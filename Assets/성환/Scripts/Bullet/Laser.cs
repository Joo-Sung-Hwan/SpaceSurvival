using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public int ReflectMaxCount { get; set; }
    int reflectcount;
    Vector2 nextDir;
    float roZ;
    public void Start()
    {
        SetData();
        ReflectMaxCount = 5;
        
    }

    void Update()
    {
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

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CameraWall"))
        {
            Vector2 income = nextDir;
            Vector2 normal = collision.contacts[0].normal;
            nextDir = Vector2.Reflect(income, normal).normalized;
            roZ = GetAngle(normal, nextDir);
            transform.rotation = Quaternion.Euler(0, 0, roZ);
            reflectcount++;
        }        
    }
    public float GetAngle(Vector2 b_dir, Vector2 a_dir)
    {
        Vector2 v = a_dir - b_dir;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    
}
