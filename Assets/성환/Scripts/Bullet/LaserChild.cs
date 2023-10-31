using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserChild : MonoBehaviour
{
    [HideInInspector] public float Attack { get; set; }
    // Start is called before the first frame update

    private void Start()
    {
        Attack = 10f;
    }

    public void Init(Vector3 a_pos, Vector3 b_pos,float laser_size)
    {
        transform.position = a_pos;
        float rotZ = transform.parent.GetComponent<Laser>().GetAngle(a_pos, b_pos);
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        GetComponent<SpriteRenderer>().size = new Vector2(Vector2.Distance(a_pos, b_pos), laser_size);
        GetComponent<BoxCollider2D>().size = new Vector2(Vector2.Distance(a_pos, b_pos), laser_size);
        GetComponent<BoxCollider2D>().offset = new Vector2((Vector2.Distance(a_pos, b_pos)) * 0.5f, 0f);
    }
}
