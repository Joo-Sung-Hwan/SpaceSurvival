using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataManager : MonoBehaviour
{
    [HideInInspector] public float laser_damage;
    [HideInInspector] public float bomb_damage;
    [HideInInspector] public float bomb_range;

    private void Start()
    {
        laser_damage = 10f;
        bomb_damage = 20f;
    }
}
