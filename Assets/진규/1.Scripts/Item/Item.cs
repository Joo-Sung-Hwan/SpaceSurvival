using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemInfo
{
    public int exp;
}
public abstract class Item : MonoBehaviour
{
    public ItemInfo info = new ItemInfo();

    public abstract void Init();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("ªË¡¶");
            gameObject.SetActive(false);
        }
    }
}
