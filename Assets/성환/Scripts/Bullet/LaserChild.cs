using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserChild : MonoBehaviour
{
    [HideInInspector] public float Attack { get; set; }
    // Start is called before the first frame update
    int num;
    private void Start()
    {
        Attack = 10f;
        num = 1;
    }

    public void Update()
    {
        LaserMove();

    }
    public void LaserMove()
    {
        LineRenderer lr = transform.parent.GetComponent<LineRenderer>();
        if (transform.position == lr.GetPosition(num))
        {
            transform.position = lr.GetPosition(num);
            Debug.Log("Á¤Áö");
            num++;
        }
        else
        {
            transform.position += new Vector3(lr.GetPosition(num).x, lr.GetPosition(num).y) * Time.deltaTime;
            float rotZ = transform.parent.GetComponent<Laser>().GetAngle(lr.GetPosition(num), lr.GetPosition(num + 1));
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
    public void SetPosition(Vector3 a_pos, Vector3 b_pos,float laser_size)
    {
        transform.position = a_pos;
        float rotZ = transform.parent.GetComponent<Laser>().GetAngle(a_pos, b_pos);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
