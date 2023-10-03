using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/*
public class TurnController1
{
	public IPlayer PlayerOne { get; set; }
	public IPlayer PlayerTwo { get; set; }

	private IPlayer _currentPlayer;
	public IPlayer CurrentPlayer
	{
		get => _currentPlayer;
		private set
		{
			_currentPlayer = value;
			StartTurn(_currentPlayer);
			OnPlayerChanged?.Invoke(value);
		}
	}
	private IPlayer XPlayer => PlayerOne.NodeType == NodeType.X ? PlayerOne : PlayerTwo;

	private Stack<Tuple<IPlayer, Vector2Int>> _movesHistory = new Stack<Tuple<IPlayer, Vector2Int>>();
	public bool AnyComputerPlay => !PlayerOne.AllowInput || !PlayerTwo.AllowInput;

	private void SwitchPlayer()
	{
		if (CurrentPlayer == PlayerOne)
		{
			CurrentPlayer = PlayerTwo;
		}
		else
		{
			CurrentPlayer = PlayerOne;
		}
	}
}
*/