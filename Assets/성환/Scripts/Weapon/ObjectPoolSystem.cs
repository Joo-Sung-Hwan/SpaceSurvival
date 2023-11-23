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
        private static Dictionary<int, Queue<T>> pools = new Dictionary<int, Queue<T>>();
       
        public static T GetPool(T value,int num, Transform trans)
        {
            T item = default(T);

            if (!pools.ContainsKey(num))
            {
                pools.Add(num, new Queue<T>());
                item = Instantiate(value, trans);
            }
            else if (pools.ContainsKey(num) && pools[num].Count ==0)
            {
                item = Instantiate(value, trans);
            }
            else
            {
                item = pools[num].Dequeue();
            }
            return item;
        }
        public static void ReturnPool(T value,int num)
        {
            pools[num].Enqueue(value);
        }
    }
}
