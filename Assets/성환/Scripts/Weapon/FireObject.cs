using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    [SerializeField]Weapon weapon;
    Dictionary<Weapon, float> createtime_dic = new();
    // Start is called before the first frame update
    void Start()
    {
        // µñ¼Å³Ê¸®¿¡ ÃÑ¾Ë Ãß°¡
        weapon.Initalize();
        SetWeapon(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    public void Fire()
    {
        foreach (KeyValuePair<Weapon, float> data in createtime_dic)
        {
            Debug.Log("123");
            createtime_dic[data.Key] += Time.deltaTime;
            if(createtime_dic[data.Key] > data.Key.weaponData.createDelay)
            {
                Weapon w = ObjectPoolSystem.ObjectPoolling<Weapon>.GetPool(data.Key, data.Key.bt, transform);
                w.Initalize();
                w.transform.SetParent(GameManager.instance.playerSpawnManager.tmp_bomb_parent);
                createtime_dic[data.Key] = 0f;
            }
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        if (!createtime_dic.ContainsKey(weapon))
        {
            createtime_dic.Add(weapon, 0);
        }
    }
}
