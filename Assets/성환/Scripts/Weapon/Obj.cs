using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    private static Obj instance = null;

    public static Obj Instance { get { return instance; } }

    private void Awake()
    {
        if (Instance == null) instance = this;
        else Destroy(gameObject);
    }
    public static class ObjectPoolSystem<T> where T : Object
    {
        private static Dictionary<string, Queue<T>> pools = new Dictionary<string, Queue<T>>();

        public static T GetPool(T value, Transform trans)
        {
            T item = default(T);

            if (!pools.ContainsKey(value.ToString()))
            {
                pools.Add(value.ToString(),new Queue<T>());
                item = Instantiate(value, trans);
            }
            else if(pools.ContainsKey(value.ToString()) && pools[value.ToString()].Count == 0)
            {
                item = Instantiate(value, trans);
            }
            else
            {
                item = pools[value.ToString()].Dequeue();
            }
            return item;
        }

        public static void ReturnPool(T value)
        {
            pools[value.ToString()].Enqueue(value);
        }
    }

}


