using UnityEngine;

public enum PlayerNumber
{
	PlayerOne,
	PlayerTwo
}

/// <summary>
/// Base player class
/// </summary>
public abstract class IPlayer : MonoBehaviour
{
	public PlayerNumber PlayerNumber { get; private set; }
	public string Name { get; private set; }
	public NodeType NodeType { get; private set; }

	public abstract bool AllowInput { get; }

	public void SetName(string name)
    {
		Name = name;
	}

	public void SetPlayerNumber(PlayerNumber playerNumber)
    {
		PlayerNumber = playerNumber;
	}

	public void SetNodeType(NodeType nodeType)
    {
		NodeType = nodeType;
	}

	/// <summary>
	/// Call this method to start this player turn
	/// </summary>
	public virtual void StartTurn() { }

	/// <summary>
	/// Call this method to end this player turn
	/// </summary>
	public virtual void EndTurn() { }

    public override string ToString()
    {
		return $"{PlayerNumber}: \"{Name}\", {NodeType}";
    }
}
