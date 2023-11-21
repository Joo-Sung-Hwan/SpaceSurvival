using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponData
{
    public float Damage { get; set; }
    public int compareNum;
}

public abstract class Weapon : MonoBehaviour
{
    private Dictionary<string, T> test = new Dictionary<string, T>();
    public WeaponData weaponData = new WeaponData();
    public Weapon weapon;
    public void Add<T>(string key, T ab) where T : Object
    {

    }

    public T GetValue<T>(string key) where T : Object
    {
        return test[key] as T;
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

            // 적에게 피해주기
            gameObject.SetActive(false);
        }
    }
}
