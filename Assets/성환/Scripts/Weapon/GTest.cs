using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTest : MonoBehaviour , ObectPool
{
    public class ObjectPoolTest<T> : GameManager
    {
        private Queue<T> poolings = new Queue<T>();

        public ObjectPoolTest(T value)
        {
            poolings.Enqueue(value);
        }
        public T GetPoolItem() 
        {
            object item = default(T);
            if (poolings.Count != 0)
            {
                return poolings.Dequeue();
            }
            else
            {
                return default(T);
            }
        }
    }
}
public interface ObectPool
{
    public class PoolsTest<T> : MonoBehaviour
    {
        private Queue<T> poolings = new Queue<T>();
        
        public  T GetPool()
        {
            return  poolings.Dequeue();
        }
        public  void ReturnPool(T value)
        {
            poolings.Enqueue(value);
        }
        public bool CheckPool()
        {
            if (poolings.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

