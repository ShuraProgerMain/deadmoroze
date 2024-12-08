using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        public static ObjectPooler init;

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Awake() 
        {
            if(!init)
            {
                init = this;
            }
            else
            {
                Destroy(gameObject);
            }

            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach(Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for(int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);

                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        private void Start()
        {
        
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if(!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Fuck");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;

        }
    }
}
