using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    [SerializeField]
    private Button startGameButton;

    private void Awake()
    {
        startGameButton.onClick.AddListener(OnStartGameButtonClick);
    }

    private void OnStartGameButtonClick()
    {
        GameManager.Instance.SetLevelState();
    }
}
