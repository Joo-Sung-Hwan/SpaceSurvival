using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSystem : MonoBehaviour
{
    private static ObjectPoolSystem instance = null;
    public static ObjectPoolSystem Instance { get { return instance; } }
    private void Awake()
    {
        if (Instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static class ObjectPoolling<T> where T : Object
    {
        private static Dictionary<WeaponName, Queue<T>> pools = new Dictionary<WeaponName, Queue<T>>();
       

        public static T GetPool(T value, WeaponName name, Transform trans)
        {
            T item = default(T);

            if (!pools.ContainsKey(name))
            {
                pools.Add(name, new Queue<T>());
                item = Instantiate(value, trans);
            }
            else
            {
                if (pools[name].Count == 0)
                {
                    item = Instantiate(value, trans);
                }
                else
                {
                    item = pools[name].Dequeue();
                }
            }
            return item;
        }

        public static void ReturnPool(T value,WeaponName name)
        {
            Debug.Log("da");
            pools[name].Enqueue(value);
        }
    }
}
