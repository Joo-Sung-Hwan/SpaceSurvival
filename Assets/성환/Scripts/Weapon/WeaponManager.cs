using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon weapon;

    private float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time > weapon.weaponData.createDelay)
        {
            // 
            time = 0f;
        }
    }
}
