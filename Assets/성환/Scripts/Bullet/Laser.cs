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
        LaserLastTime = 10f;
        SetData();
    }

    void FixedUpdate()
    {
        DestroyLaser();
        //Debug.Log(ReflectMaxCount);
    }

    // ������ ����
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
        // ������ �߻� ù ������ �׵θ� 4���� ���� ��ǥ
        nextDir = GameManager.instance.GetRandomPosition(GameManager.instance.spawnManager.spawnPoint[rand].transform, GameManager.instance.spawnManager.spawnPoint[rand]).normalized;
        ShootRay();
    }

    // LineRenderer, ������ �߻�
    public void ShootRay()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        Vector2 newposition = transform.position;
        Vector2 newDir = nextDir;
        lr.positionCount = ReflectMaxCount;
        // LineRenderer�� ��� ��ǥ ������ŭ Laserchild ����
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
            OnCollider();
            // ray�� collider�� �ɸ��� ��� �����ϱ� ���� ����
            ray.collider.gameObject.SetActive(false);
            // ���� ������ �ݻ���Ѽ� ����
            newDir = Vector2.Reflect(newDir, ray.normal);
        }
        OnCollider();
        lr.enabled = false;
    }

    public LaserChild CreateLaser()
    {

        LaserChild l = GameManager.instance.pollingsystem.PollingLaserChild(lc, transform);
        return l;
    }
    // �ݶ��δ� SetActive
    public void OnCollider()
    {
        for (int i = 0; i < 4; i++)
        {
            GameManager.instance.spawnManager.spawnPoint[i].gameObject.SetActive(true);
        }
    }

    // �� ���� ������ ����
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