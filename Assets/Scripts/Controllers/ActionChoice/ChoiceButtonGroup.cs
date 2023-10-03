using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceButtonGroup : MonoBehaviour
{
    private List<GameModeChoiceButton> _gameModeChoiceButtons;
    private GameModeChoiceButton _selectedButton =>
        _gameModeChoiceButtons.FirstOrDefault(button => button.Selected);

    public bool AnyButtonSelected => _gameModeChoiceButtons.Any(button => button.Selected);

    private void Awake()
    {
        _gameModeChoiceButtons = GetComponentsInChildren<GameModeChoiceButton>().ToList();
        SetBaseOnClickAction();
    }

    private void Start()
    {
    }

    private void SetBaseOnClickAction()
    {
        AddListener(DeselectAll);
        _gameModeChoiceButtons.ForEach(button => button.Button.onClick.AddListener(button.Select));
    }

    public void ExecuteSelected()
    {
        _selectedButton?.ActionChoice.Excecute();
    }

    public void DeselectAll() =>
        _gameModeChoiceButtons.ForEach(button => button.Deselect());

    public void AddListener(UnityAction call) =>
        _gameModeChoiceButtons.ForEach(button => button.Button.onClick.AddListener(call));
}
