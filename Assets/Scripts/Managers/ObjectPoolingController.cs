using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ObjectPoolingController : MonoBehaviour
{
    [SerializeField]
    private List<Pool> pools = new List<Pool>();

	[Inject]
	private DiContainer _diContainer;

    private void Start()
    {
        foreach (var pool in pools)
        {
            for (int i = 0; i < pool.StartPoolCount; i++)
            {
                var newObject = _diContainer.InstantiatePrefab(pool.PoolObjectPrefab, pool.Parent).GetComponent<BasePoolableController>();
                newObject.gameObject.SetActive(false);
                SetObjectName(newObject.gameObject);
                pool.PooledObjects.Enqueue(newObject);
            }
        }
    }

    private void SetObjectName(GameObject poolableObject)
    {
        poolableObject.name = poolableObject.name.Replace("(Clone)", $"{poolableObject.GetInstanceID()}");
    }

    /// <summary>
    /// Returns pooled object of given type
    /// </summary>
    /// <param name="poolableType">Name of pooled object set in Pool component</param>
    /// <returns></returns>
    public BasePoolableController GetFromPool(string poolableType)
    {
        var pool = GetPoolByPoolableNameType(poolableType);
        if (pool == null)
        {
            Debug.LogError($"There is no pool of {poolableType} type!");
            return null;
        }
        if (pool.PooledObjects.Count > 0)
        {
            var newObject = pool.PooledObjects.Dequeue();
            pool.ObjectsOutsidePool.Add(newObject);
            return newObject;
        }
        else
        {
            if (pool.CanGrow)
            {
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.Parent);
                SetObjectName(newObject.gameObject);
                pool.ObjectsOutsidePool.Add(newObject);
                return newObject;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Return all objects to their pools
    /// </summary>
    public void ReturnAllToPools()
    {
        foreach (var pool in pools)
        {
            pool.ReturnAllToPool();
        }
    }

    /// <summary>
    /// Return given object to its pool
    /// </summary>
    /// <param name="objectToReturn">Object to return</param>
    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        var pool = GetPoolByPoolableNameType(objectToReturn.PoolableType);
        pool.ReturnToPool(objectToReturn);
    }

    private Pool GetPoolByPoolableNameType(string poolableType)
        => pools.FirstOrDefault(x => x.PoolableNameType.Equals(poolableType));
}
