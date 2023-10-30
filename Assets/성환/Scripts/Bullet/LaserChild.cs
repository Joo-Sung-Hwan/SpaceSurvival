using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserChild : MonoBehaviour
{
    [HideInInspector] public float Attack { get; set; }

    // Start is called before the first frame update

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Attack = GameManager.instance.weaponDataManager.laser_damage;

    }
}
