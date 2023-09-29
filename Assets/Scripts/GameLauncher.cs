using UnityEngine;

public class GameLauncher : BaseManager<GameLauncher>
{
    [SerializeField]
    private Transform gamePlane;
    public Transform GamePlane => gamePlane;

    [SerializeField]
    private Canvas canvas;
    public Canvas Canvas => canvas;
}
