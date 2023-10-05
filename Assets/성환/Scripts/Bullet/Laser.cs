using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [HideInInspector] public int ReflectMaxCount { get; set; }
    int reflectcount;
    public void Start()
    {
        SetData();
        ReflectMaxCount = 2;
    }

    public void Update()
    {
        DestroyLaser();
    }
    public void DestroyLaser()
    {
        if(reflectcount >= ReflectMaxCount)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void SetData()
    {
        reflectcount = 0;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CameraWall"))
        {
            reflectcount++;
            Debug.Log(reflectcount);
        }
        else
        {
            return;
        }
    }
}
