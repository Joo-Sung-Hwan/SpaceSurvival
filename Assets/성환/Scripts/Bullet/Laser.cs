using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public int ReflectMaxCount { get; set; }
    [HideInInspector] public float LaserLastTime { get; set; }
    public LaserChild lc;
    public LayerMask layermask;
    Vector2 nextDir;
    RaycastHit2D ray;
    LineRenderer lr;
    float d_time = 0;
    [HideInInspector] public float LaserSize { get; set; }
    public void Start()
    {
        ReflectMaxCount = 5;
        LaserSize = 0.2f;
        LaserLastTime = 0.5f;
        SetData();
    }

    void FixedUpdate()
    {
        DestroyLaser();
        //Debug.Log(ReflectMaxCount);
    }

    // 레이저 삭제
    public void DestroyLaser()
    {
        d_time += Time.deltaTime;
        if (d_time > LaserLastTime)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
            d_time = 0f;
        }
    }

    public void SetData()
    {
        int rand = Random.Range(0, 4);
        SetLaserSizeY(LaserSize);
        // 레이저 발사 첫 방향은 테두리 4방향 랜덤 좌표
        nextDir = GameManager.instance.GetRandomPosition(GameManager.instance.spawnManager.spawnPoint[rand].transform, GameManager.instance.spawnManager.spawnPoint[rand]).normalized;
        ShootRay();
    }

    // LineRenderer, 레이저 발사
    public void ShootRay()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        Vector2 newposition = transform.position;
        Vector2 newDir = nextDir;
        lr.positionCount = ReflectMaxCount;
        // LineRenderer의 찍는 좌표 갯수만큼 반복문 호출
        for (int i = 0; i < lr.positionCount; i++)
        {
            if (i == 0)
            {
                
                lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0f));
                continue;
            }
            ray = Physics2D.Raycast(newposition, newDir, 300f, layermask);
            lr.SetPosition(i, new Vector3(ray.point.x, ray.point.y, 0f));
            newposition = ray.point;
            LaserChild c = GameManager.instance.pollingsystem.PollingLaserChild(lc, transform);
            c.Init(lr.GetPosition(i - 1), lr.GetPosition(i), LaserSize);
            OnCollider();
            ray.collider.gameObject.SetActive(false);
            newDir = Vector2.Reflect(newDir, ray.normal);
        }
        OnCollider();
        lr.enabled = false;
    }

    // 콜라인더 SetActive
    public void OnCollider()
    {
        for (int i = 0; i < 4; i++)
        {
            GameManager.instance.spawnManager.spawnPoint[i].gameObject.SetActive(true);
        }
    }

    public float GetAngle(Vector2 b_dir, Vector2 a_dir)
    {
        Vector2 v = a_dir - b_dir;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    public void SetLaserSizeY(float sizeY)
    {
        LaserSize = sizeY;
    }


}