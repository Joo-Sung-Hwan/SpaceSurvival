using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public int ReflectMaxCount { get; set; }
    public LaserChild lc;
    int reflectcount;
    Vector2 nextDir;
    float roZ;
    RaycastHit2D ray;
    RaycastHit ray2;
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
        DestroyLaser();
    }
    public void DestroyLaser()
    {
        if (reflectcount >= ReflectMaxCount)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetData()
    {
        int rand = Random.Range(0, 4);
        reflectcount = 0;

        // 레이저 발사 첫 방향은 테두리 4방향 랜덤 좌표
        nextDir = GameManager.instance.GetRandomPosition(GameManager.instance.spawnManager.spawnPoint[rand].transform, GameManager.instance.spawnManager.spawnPoint[rand]).normalized;
        ShootRay();
    }

    // LineRenderer, 레이저 발사
    public void ShootRay()
    {
        Vector2 newposition = transform.position;
        Vector2 newDir = nextDir;
        lr.positionCount = ReflectMaxCount;
        float rotZ;
        for (int i = 0; i < lr.positionCount; i++)
        {
            if (i == 0)
            {
                lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0f));
                continue;
            }
            ray = Physics2D.Raycast(newposition, newDir, 300f, layermask);
            LaserChild c = Instantiate(lc, transform);
            lr.SetPosition(i, new Vector3(ray.point.x, ray.point.y, 0f));
            newposition = ray.point;
            c.transform.position = lr.GetPosition(i - 1);
            rotZ = GetAngle(lr.GetPosition(i - 1), lr.GetPosition(i));
            c.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            c.GetComponent<SpriteRenderer>().size = new Vector2(Vector2.Distance(lr.GetPosition(i - 1), lr.GetPosition(i)) + 1f, 0.3f);
            // Debug.Log($"i = {i}, 시작지점 : {c.transform.position}, 다음지점 : {lr.GetPosition(i)}, Rotation = {rotZ} ");
            OnCollider();
            ray.collider.gameObject.SetActive(false);
            newDir = Vector2.Reflect(newDir, ray.normal);

        }
        OnCollider();
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



}