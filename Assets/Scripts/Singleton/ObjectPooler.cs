using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Object Pooler Class
/// </summary>
public class ObjectPooler : Singleton<ObjectPooler>
{
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    private Transform objectPool;

    /// <summary>
    /// Create Object Pool
    /// </summary>
    void OnEnable()
    {
        objectPool = new GameObject("Object Pool").transform;

        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool, objectPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    /// <summary>
    /// Returns a pooled object by tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool, objectPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}

/// <summary>
/// An item for Object Pooling
/// </summary>
[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}