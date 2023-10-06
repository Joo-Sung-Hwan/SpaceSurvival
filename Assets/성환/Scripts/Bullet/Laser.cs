using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public int ReflectMaxCount { get; set; }
    int reflectcount;
    Vector2 triggerVec;
    Vector2 nextDir;
    float roZ;

    RaycastHit2D hit;
    public void Start()
    {
        SetData();
        ReflectMaxCount = 5;
    }

    public void Update()
    {
        LaserMove(nextDir);
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
        GetRayPosition();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CameraWall"))
        {
            reflectcount++;
        }
        
    }

    public void LaserMove(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(nextDir * Time.deltaTime * 10f);
        
    }

    public float GetAngle(Vector2 b_dir, Vector2 a_dir)
    {
        Vector2 v = a_dir - b_dir;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    public void GetRayPosition()
    {
        hit = Physics2D.Raycast(transform.position, nextDir);
        if (hit.collider.gameObject.CompareTag("CameraWall"))
        {
            Debug.Log(hit.point);
        }
    }
}
