using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasController : BaseManager<CanvasController>
{
    [SerializeField]
    private Transform menusParent;
    public Transform MenusParent => menusParent;

    public Canvas Canvas { get; private set; }

    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
    }
}
