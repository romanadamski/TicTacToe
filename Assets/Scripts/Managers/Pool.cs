using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Pool
{
    public int StartPoolCount;
    public BasePoolableController PoolObjectPrefab;
    public bool CanGrow;
    public Queue<BasePoolableController> PooledObjects = new Queue<BasePoolableController>();
    public bool CustomParent => PoolObjectPrefab.CustomParent;

    [HideInInspector]
    public int ObjectCount;
    [HideInInspector]
    public Transform Parent;

    /// <summary>
    /// Type choosen in prefab from Poolable type dropdown
    /// </summary>
    public string PoolableNameType => PoolObjectPrefab.PoolableType;

    [HideInInspector]
    public List<BasePoolableController> ObjectsOutsidePool = new List<BasePoolableController>();

    public void ReturnAllToPool()
    {
        foreach (var poolObject in ObjectsOutsidePool.ToList())
        {
            ReturnToPool(poolObject);
        }
        ObjectsOutsidePool.Clear();
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        if (!PooledObjects.Contains(objectToReturn))
        {
            objectToReturn.gameObject.SetActive(false);
            PooledObjects.Enqueue(objectToReturn);
            objectToReturn.OnReturnToPool();
        }
    }
}