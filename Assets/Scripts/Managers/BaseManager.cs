using UnityEngine;

/// <summary>
/// Base class for managers providing singleton pattern
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
 
    /// <summary>
    /// Instance of <typeparamref name="T"/>
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            return _instance;
        }
    }
}
