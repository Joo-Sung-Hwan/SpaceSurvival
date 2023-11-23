using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserChild : MonoBehaviour
{
    [HideInInspector] public float Attack { get; set; }
    float lerp_time;
    int num;
    LineRenderer lr;
    // Start is called before the first frame update
    
    private void Start()
    {
        SetData();
    }

    public void Update()
    {
        lerp_time += Time.deltaTime;
        if(lerp_time >= 1)
        {
            num++;
            lerp_time = 0f;
        }
        if(num == 4)
        {
            return;
        }
        transform.position = Vector3.Lerp(lr.GetPosition(num), lr.GetPosition(num + 1), lerp_time);
        float rotZ = transform.parent.GetComponent<Laser>().GetAngle(lr.GetPosition(num), lr.GetPosition(num + 1));
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
    
    public void SetData()
    {
        lr = transform.parent.GetComponent<LineRenderer>();
        Attack = 10f;
        num = 0;
        lerp_time = 0f;
    }
    public void SetPosition(Vector3 a_pos, Vector3 b_pos,float laser_size)
    {
        transform.position = a_pos;
        float rotZ = transform.parent.GetComponent<Laser>().GetAngle(a_pos, b_pos);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    
}
