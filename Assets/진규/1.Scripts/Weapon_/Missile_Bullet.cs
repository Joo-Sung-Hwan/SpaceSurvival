using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Missile_Bullet : Weapon
{
    Vector3[] point = new Vector3[4];

    [SerializeField] [Range(0,1)] private float t = 0;
    [SerializeField] public float spd = 0.5f;
    [SerializeField] public float posA = 0.55f;
    [SerializeField] public float posB = 0.45f;
    [SerializeField] private float timerMax = 0;
    [SerializeField] private float timerCurrent = 0;
    public Enemy target;
    [Space(10f)]
    public float disStart = 3.0f;
    public float disEnd = 1.0f;

    //public GameObject master;
    //public GameObject enemy;

    void Start()
    {
        /*point[0] = master.transform.position;
        point[1] = PointSetting(master.transform.position);
        point[2] = PointSetting(enemy.transform.position);
        point[3] = enemy.transform.position;*/
    }

    public override void Attack()
    {
        /*if (t > 1)
            return;
        t += Time.deltaTime * 0.7f;
        //DrawTrajectory();*/
        if (timerCurrent > timerMax)
            return;
        timerCurrent += Time.deltaTime * 1;
        Find();
        //transform.position += DrawTrajectory() * Time.deltaTime * 1f;
        //DrawTrajectory();
        //transform.Translate(Vector3.forward * Time.deltaTime * spd);
    }

    /*Vector2 PointSetting(Vector2 origin)
    {
        float x, y;
        x = posA * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.x;
        y = posB * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.y;
        return new Vector2(x, y);
    }*/
    void Find()
    {
        Missile m = GameManager.instance.player.missile.GetComponent<Missile>();

        Vector2 vec = transform.position - m.FindE().transform.position;
        Vector3 axis = Vector3.Cross(vec, transform.forward);
        Quaternion.AngleAxis(90, axis);
        Quaternion rot = Quaternion.AngleAxis(Time.deltaTime * 45, axis);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 50 * Time.deltaTime);
        transform.Translate(DrawTrajectory() * Time.deltaTime * 1f);
    }
    IEnumerator Draw()
    {
        transform.position = new Vector3(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
            FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y),
            FourPointBezier(point[0].z, point[1].z, point[2].z, point[3].z));
        yield return new WaitForSeconds(2f);
        ObjectPoolSystem.ObjectPoolling<Weapon>.ReturnPool(this, ObjectName.Missile);

    }
    public Vector3 DrawTrajectory()
    {
        /*transform.position = new Vector2(FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
                    FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y));*/
        return transform.position = new Vector3(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
            FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y),
            FourPointBezier(point[0].z, point[1].z, point[2].z, point[3].z));
    }

    public void Init(Transform _startTr, Transform _endTr, float _speed, float _newPointDistanceFromStartTr, float _newPointDistanceFromEndTr)
    {
        spd = _speed;

        // ���� ������ �ð��� �������� ��.
        timerMax = Random.Range(0.8f, 1.0f);

        // ���� ����.
        point[0] = _startTr.position;

        // ���� ������ �������� ���� ����Ʈ ����.
        point[1] = _startTr.position +
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * _startTr.right) + // X (��, �� ��ü)
            (_newPointDistanceFromStartTr * Random.Range(-0.15f, 1.0f) * _startTr.up) + // Y (�Ʒ��� ����, ���� ��ü)
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, -0.8f) * _startTr.forward); // Z (�� �ʸ�)

        // ���� ������ �������� ���� ����Ʈ ����.
        point[2] = _endTr.position +
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.right) + // X (��, �� ��ü)
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.up) + // Y (��, �Ʒ� ��ü)
            (_newPointDistanceFromEndTr * Random.Range(0.8f, 1.0f) * _endTr.forward); // Z (�� �ʸ�)

        // ���� ����.
        point[3] = _endTr.position;

        transform.position = _startTr.position;
    }
    float FourPointBezier(float a, float b, float c, float d)
    {
        /*return Mathf.Pow((1 - t), 3) * a 
            + Mathf.Pow((1 - t), 2) * 3 * t * b 
            + Mathf.Pow(t,2) * 3 * (1 - t) * c
            + Mathf.Pow(t,3) * d;*/
        float t = timerCurrent / timerMax;

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }

    public override void Initalize()
    {
        weaponData.Damage = 10f;
        objectName = ObjectName.Missile;
        //Init(m.transform, m.FindE(), spd, disStart, disEnd);
    }

    public override void PlayAct(Collider2D collider)
    {
        Enemy e = collider.GetComponent<Enemy>();
        e.Hp -= weaponData.Damage;
        e.CreateDamageTxt(weaponData.Damage);
        ObjectPoolSystem.ObjectPoolling<Weapon>.ReturnPool(this, ObjectName.Missile);
    }
}
