using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    private GameplayEventsSO gameplayEventsSO;
    [SerializeField]
    private BoardEventsSO boardEventsSO;

    //public BoardController BoardController { get; set; }

    private void Awake()
    {
    }

}
