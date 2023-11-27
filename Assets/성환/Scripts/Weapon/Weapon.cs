using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponData
{
    public float Damage { get; set; }
    public float Range { get; set; }
    public float Debuff { get; set; }
    public float destroyTime;
    public float createDelay;
    public byte compareNum;
    public ObjectName name;
    public Transform zoneTrans;
    public CircleCollider2D collider2D;
}

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData = new WeaponData();
    public Weapon weapon;
    public Coroutine coroutine;
    public ObjectName objectName = new();
    //public ObjectName bt = new ObjectName();

    bool looseZone = true;
    private void OnEnable()
    {
        if (coroutine == null)
        {
            //coroutine = StartCoroutine(DestroyTime(weaponData.destroyTime));
        }
    }
    private void OnDisable()
    {
       
    }
    public abstract void Initalize();

    private void FixedUpdate()
    {
        Attack();
    }

    public virtual void Attack()
    {
        if (GameManager.instance.isPause)
            return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy[] enemys = FindObjectsOfType<Enemy>();
        if (collision.CompareTag("Enemy"))
        {
            Enemy e = collision.GetComponent<Enemy>();
            switch(objectName)
            {
                case ObjectName.Bomb:
                    switch (GameManager.instance.player.bomb.tag)
                    {
                        case "MagnetBomb":
                            TransChange(collision, out e.magnetBombZone);
                            break;
                        case "WebBomb":
                            TransChange(collision, out e.webBombZone);
                            StartCoroutine(e.OnOff(gameObject));
                            e.DeBuff = weaponData.Debuff;
                            break;
                    }
                    break;
            }
        }
    }

    private void TransChange(Collider2D collider, out bool zone)
    {
        collider.GetComponent<Enemy>().typeTrans = transform;
        zone = true;
    }

    private IEnumerator DestroyTime(float time)
    {
        // 오브젝트의 최대 지속시간
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
