using System;
using System.Collections.Generic;

public interface ITurnController
{
	IPlayer CurrentPlayer { get; }
	void SetFirstPlayer();
	List<IPlayer> Players { get; }
	void SetNextPlayer();
	void AddPlayer(IPlayer player);
	void UndoTurn();
}
