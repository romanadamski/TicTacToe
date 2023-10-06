using System;
using System.Collections.Generic;

public interface ITurnController
{
	IPlayer CurrentPlayer { get; }
	bool AnyComputerPlay { get; }
	List<IPlayer> Players { get; }
	void SetFirstPlayer();
	void SetNextPlayer();
	void AddPlayer(IPlayer player);
	void UndoTurn();
}
