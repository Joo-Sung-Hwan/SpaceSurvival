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
        if (collision.CompareTag("Enemy"))
        {
            PlayAct(collision);
        }
    }

    public abstract void PlayAct(Collider2D collider);

    private IEnumerator DestroyTime(float time)
    {
        // 오브젝트의 최대 지속시간
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
