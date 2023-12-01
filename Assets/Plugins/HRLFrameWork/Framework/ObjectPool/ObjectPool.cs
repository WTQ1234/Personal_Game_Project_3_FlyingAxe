using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HRL
{
    public class ObjectPool
    {
        public GameObject prefab;
        public Transform parent;
        public int poolSize = 10;

        private List<GameObject> pool = new List<GameObject>();

        public ObjectPool(GameObject prefab, Transform _parent, int poolSize)
        {
            this.prefab = prefab;
            this.parent = _parent;
            this.poolSize = poolSize;
            this.pool = new List<GameObject>(poolSize);
        }

        public GameObject Get()
        {
            if (pool.Count > 0)
            {
                GameObject t = pool[pool.Count - 1];
                pool.RemoveAt(pool.Count - 1);
                t.gameObject.SetActive(true);
                return t;
            }
            GameObject obj = GameObject.Instantiate<GameObject>(prefab, parent);
            obj.transform.name = prefab.name;
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }

        // ÿ��һ��ʱ����м�⣬�������size�����������Ԥ��
        public void DetectTick()
        {
            if (pool.Count > poolSize * 2)
            {
                // TODO: û������
                pool.RemoveRange(poolSize, pool.Count - poolSize);
            }
        }
    }
}
