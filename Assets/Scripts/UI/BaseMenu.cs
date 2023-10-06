using UnityEngine;

/// <summary>
/// Base class for UI elements: HUDs, menu ect.
/// </summary>
public class BaseMenu : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
