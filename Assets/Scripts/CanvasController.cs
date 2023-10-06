using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasController : BaseManager<CanvasController>
{
    [SerializeField]
    private Transform menusParent;
    public Transform MenusParent => menusParent;

    [SerializeField]
    public Canvas canvas;
    public Canvas Canvas => canvas;
}
