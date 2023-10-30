using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataManager : MonoBehaviour
{
    [HideInInspector] public float laser_damage;
    [HideInInspector] public float normal_bomb_damage;

    private void Start()
    {
        laser_damage = 10f;
        normal_bomb_damage = 20f;
    }
}
