using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        private static Dictionary<ObjectName, Queue<T>> pools = new Dictionary<ObjectName, Queue<T>>();
        public static T GetPool(T value, ObjectName name, Transform trans)
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
                    item.GameObject().transform.position = trans.position;
                    item.GameObject().SetActive(true);
                }
            }
            return item;
        }
        public static void ReturnPool(T value, ObjectName name)
        {
            value.GameObject().SetActive(false);
            pools[name].Enqueue(value);
        }
    }
}
