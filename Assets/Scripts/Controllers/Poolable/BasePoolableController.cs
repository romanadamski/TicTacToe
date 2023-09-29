using UnityEngine;

public class BasePoolableController : MonoBehaviour
{
    [SerializeField]
    private bool customParent;
    public bool CustomParent => customParent;

    public string PoolableType;
    public virtual void OnReturnToPool() { }
}
