using UnityEngine;

public class BasePoolableController : MonoBehaviour
{
    public string PoolableType;
    public virtual void OnReturnToPool() { }
}
