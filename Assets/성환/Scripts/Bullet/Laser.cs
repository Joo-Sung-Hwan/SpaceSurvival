using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public int ReflectMaxCount { get; set; }
    public LaserChild lc;
    public LayerMask layermask;

    Vector2 nextDir;
    RaycastHit2D ray;
    LineRenderer lr;
    float d_time = 0;
    [HideInInspector] public float LaserSize { get; set; }
    public void Start()
    {
        lr = GetComponent<LineRenderer>();
        ReflectMaxCount = 5;
        LaserSize = 0.2f;
        SetData();
    }

    void Update()
    {
        DestroyLaser();
    }

    // 레이저 삭제
    public void DestroyLaser()
    {
        d_time += Time.deltaTime;
        if (d_time > 0.5f)
        {
            for(int i = 0; i < transform.childCount; i++)
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
        lr.enabled = true;
        Vector2 newposition = transform.position;
        Vector2 newDir = nextDir;
        lr.positionCount = ReflectMaxCount;
        float rotZ;
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
            if (transform.childCount < ReflectMaxCount - 1)
            {
                LaserChild c = Instantiate(lc, transform);
                c.transform.position = lr.GetPosition(i - 1);
                rotZ = GetAngle(lr.GetPosition(i - 1), lr.GetPosition(i));
                c.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                c.GetComponent<SpriteRenderer>().size = new Vector2(Vector2.Distance(lr.GetPosition(i - 1), lr.GetPosition(i)), LaserSize);
                transform.GetChild(i - 1).GetComponent<BoxCollider2D>().size = new Vector2(Vector2.Distance(lr.GetPosition(i - 1), lr.GetPosition(i)), LaserSize);
                transform.GetChild(i - 1).GetComponent<BoxCollider2D>().offset = new Vector2((Vector2.Distance(lr.GetPosition(i - 1), lr.GetPosition(i))) * 0.5f, 0f);
            }
            if (!transform.GetChild(i - 1).gameObject.activeSelf)
            {
                transform.GetChild(i - 1).transform.position = lr.GetPosition(i - 1);
                rotZ = GetAngle(lr.GetPosition(i - 1), lr.GetPosition(i));
                transform.GetChild(i - 1).transform.rotation = Quaternion.Euler(0, 0, rotZ);
                transform.GetChild(i - 1).GetComponent<SpriteRenderer>().size = new Vector2(Vector2.Distance(lr.GetPosition(i - 1), lr.GetPosition(i)), LaserSize);
                transform.GetChild(i - 1).GetComponent<BoxCollider2D>().size = new Vector2(Vector2.Distance(lr.GetPosition(i - 1), lr.GetPosition(i)), LaserSize);
                transform.GetChild(i - 1).GetComponent<BoxCollider2D>().offset = new Vector2((Vector2.Distance(lr.GetPosition(i - 1), lr.GetPosition(i))) * 0.5f, 0f);
                transform.GetChild(i - 1).gameObject.SetActive(true);
            }
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