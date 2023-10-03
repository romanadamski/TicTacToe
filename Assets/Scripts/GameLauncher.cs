using UnityEngine;
using Zenject;

public class GameLauncher : BaseManager<GameLauncher>
{
    [SerializeField]
    private Transform gamePlane;
    public Transform GamePlane => gamePlane;

    [SerializeField]
    private Transform menusParent;
    public Transform MenusParent => menusParent;

    [SerializeField]
    private Canvas canvas;
    public Canvas Canvas => canvas;

    [Inject]
    private SaveManager _saveManager;

    private void Awake()
    {
        _saveManager.LoadSave();
    }
}
