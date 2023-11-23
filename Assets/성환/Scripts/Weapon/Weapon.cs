using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BombType
{
    Nomal,
    Magnet,
    Web,
    Fire
}

public struct WeaponData
{
    public float Damage { get; set; }
    public float destroyTime;
    public float createDelay;
    public byte compareNum;
}

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData = new WeaponData();
    public Weapon weapon;
    public Coroutine coroutine;
    public BombType bt;

    private void OnEnable()
    {
        if (coroutine == null)
        {
            //coroutine = StartCoroutine(DestroyTime(weaponData.destroyTime));
        }
    }
    private void OnDisable()
    {
        if (coroutine != null)
        {
            //StopCoroutine(coroutine);
            //coroutine = null;
        }
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
            //collision.GetComponent<Enemy>()
            // 적에게 피해주기
            //gameObject.SetActive(false);
        }
    }
    private IEnumerator DestroyTime(float time)
    {
        // 오브젝트의 최대 지속시간
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
