using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Groups <typeparamref name="GameModeChoiceButton"/>s and provides current selected one
/// </summary>
public class ChoiceButtonGroup : MonoBehaviour
{
    private List<ChoiceButton> _gameModeChoiceButtons;
    private ChoiceButton _selectedButton =>
        _gameModeChoiceButtons.FirstOrDefault(button => button.Selected);

    /// <summary>
    /// True if any <typeparamref name="GameModeChoiceButton"/> is selected
    /// </summary>
    public bool AnyButtonSelected => _gameModeChoiceButtons.Any(button => button.Selected);

    private void Awake()
    {
        _gameModeChoiceButtons = GetComponentsInChildren<ChoiceButton>().ToList();
        SetBaseOnClickAction();
    }

    private void SetBaseOnClickAction()
    {
        AddListener(DeselectAll);
        _gameModeChoiceButtons.ForEach(button => button.AddListener(button.Select));
    }

    /// <summary>
    /// Execute action on selected <typeparamref name="GameModeChoiceButton"/>
    /// </summary>
    public void ExecuteSelected()
    {
        _selectedButton.ActionChoice.Excecute();
    }

    /// <summary>
    /// Deselect all <typeparamref name="GameModeChoiceButton"/>s within this group
    /// </summary>
    public void DeselectAll() =>
        _gameModeChoiceButtons.ForEach(button => button.Deselect());

    /// <summary>
    /// Add listener to all <typeparamref name="GameModeChoiceButton"/>s within this group
    /// </summary>
    /// <param name="call">Action to call on <typeparamref name="GameModeChoiceButton"/>s press</param>
    public void AddListener(UnityAction call) =>
        _gameModeChoiceButtons.ForEach(button => button.AddListener(call));
}
