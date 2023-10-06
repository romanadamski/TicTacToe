using UnityEngine;

public class BoardTurnController : TurnController
{
	[SerializeField]
	private TurnStateSO turnStateSO;

	private IPlayer PlayerOne => turnStateSO.PlayerOne;
	private IPlayer PlayerTwo => turnStateSO.PlayerTwo;

	protected override void SetLoser(IPlayer loser)
	{
		if (loser == PlayerOne)
		{
			gameplayEventsSO.GameOver(PlayerTwo);
		}
		else if (loser == PlayerTwo)
		{
			gameplayEventsSO.GameOver(PlayerOne);
		}
	}

	protected override void SetPlayers()
    {
		AddPlayer(PlayerOne);
		AddPlayer(PlayerTwo);
		AssignRandomNodesToPlayers();
		AssignNumbersToPlayers();
		SetPlayersNames();
	}

    private void AssignNumbersToPlayers()
	{
		PlayerOne.SetPlayerNumber(PlayerNumber.PlayerOne);
		PlayerTwo.SetPlayerNumber(PlayerNumber.PlayerTwo);
	}

	private void SetPlayersNames()
	{
		var playerName = PlayerOne.AllowInput ? "Human" : "Computer";
		if (PlayerOne.GetType().Equals(PlayerTwo.GetType()))
		{
			playerName += " 1";
		}
		PlayerOne.SetName(playerName);

		playerName = PlayerTwo.AllowInput ? "Human" : "Computer";
		if (PlayerTwo.GetType().Equals(PlayerOne.GetType()))
		{
			playerName += " 2";
		}
		PlayerTwo.SetName(playerName);
	}

	private void AssignRandomNodesToPlayers()
	{
		var random = Random.Range(0, 2);
		var playerOneNodeType = random == 0 ? NodeType.X : NodeType.O;
		var playerTwoNodeType = playerOneNodeType == NodeType.O ? NodeType.X : NodeType.O;
		PlayerOne.SetNodeType(playerOneNodeType);
		PlayerTwo.SetNodeType(playerTwoNodeType);
	}
}
