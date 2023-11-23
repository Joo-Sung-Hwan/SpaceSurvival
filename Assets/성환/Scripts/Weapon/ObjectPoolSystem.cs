using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSystem<T> : MonoBehaviour where T : Object
{
    private static ObjectPoolSystem<T> instance = null;

    public static ObjectPoolSystem<T> Instance { get { return instance; } }

    private Dictionary<string, Queue<T>> pools = new Dictionary<string, Queue<T>>();
    private void Awake()
    {
        if (Instance == null) instance = this;
        else Destroy(gameObject);
    }
    public T GetPool(T value , Transform trans)
    {
        T item = default(T);

        if (!pools.ContainsKey(value.ToString()))
        {
            pools.Add(value.ToString(), new Queue<T>());
            item = Instantiate(value, trans);
        }
        else
        {
            item = pools[value.ToString()].Dequeue();
        }
        return item;
    }
    public void ReturnPool(T value)
    {
        pools[value.ToString()].Enqueue(value);
    }
}
