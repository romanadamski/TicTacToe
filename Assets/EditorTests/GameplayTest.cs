using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameplayTest
    {
		private BoardController _ticTacToeController;

		[SetUp]
		public void Setup()
		{
			_ticTacToeController = new BoardController(3,3,3);
		}

		[Test]
		public void CheckWin_ThreeVerticalO_OWins()
		{
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(0, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(0, 1), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(0, 2), NodeType.O);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(0, 2), NodeType.O);

			// Assert
			Assert.AreEqual(NodeType.O, winner);
		}

		[Test]
		public void CheckWin_ThreeHorizontalO_OWins()
		{
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(0, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(1, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(2, 0), NodeType.O);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(0, 0), NodeType.O);

			// Assert
			Assert.AreEqual(NodeType.O, winner);
		}

		[Test]
		public void CheckWin_ThreeDiagonal1O_OWins()
		{
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(0, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(1, 1), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(2, 2), NodeType.O);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(2, 2), NodeType.O);

			// Assert
			Assert.AreEqual(NodeType.O, winner);
		}

		[Test]
		public void CheckWin_ThreeDiagonal2O_OWins()
		{
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(2, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(1, 1), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(0, 2), NodeType.O);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(0, 2), NodeType.O);

			// Assert
			Assert.AreEqual(NodeType.O, winner);
		}

		[Test]
		public void CheckLose_ThreeVerticalO_XLose()
		{
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(0, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(0, 1), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(0, 2), NodeType.O);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(0, 2), NodeType.O);

			// Assert
			Assert.AreNotEqual(NodeType.X, winner);
			Assert.AreNotEqual(NodeType.None, winner);
		}

		[Test]
		public void CheckLose_ThreeHorizontalO_XLose()
		{
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(0, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(1, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(2, 0), NodeType.O);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(0, 0), NodeType.O);

			// Assert
			Assert.AreNotEqual(NodeType.X, winner);
			Assert.AreNotEqual(NodeType.None, winner);
		}

		[Test]
		public void CheckLose_ThreeDiagonal1O_XLose()
		{
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(0, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(1, 1), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(2, 2), NodeType.O);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(2, 2), NodeType.O);

			// Assert
			Assert.AreNotEqual(NodeType.X, winner);
			Assert.AreNotEqual(NodeType.None, winner);
		}

		[Test]
		public void CheckLose_ThreeDiagonal2O_XLose()
		{
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(2, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(1, 1), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(0, 2), NodeType.O);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(0, 2), NodeType.O);

			// Assert
			Assert.AreNotEqual(NodeType.X, winner);
			Assert.AreNotEqual(NodeType.None, winner);
		}

		[Test]
        public void CheckDraw()
        {
			// Arrange
			_ticTacToeController.SetNode(new Vector2Int(0, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(1, 0), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(2, 0), NodeType.X);
			_ticTacToeController.SetNode(new Vector2Int(0, 1), NodeType.X);
			_ticTacToeController.SetNode(new Vector2Int(1, 1), NodeType.X);
			_ticTacToeController.SetNode(new Vector2Int(2, 1), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(0, 2), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(1, 2), NodeType.O);
			_ticTacToeController.SetNode(new Vector2Int(2, 2), NodeType.X);

			// Act
			var winner = _ticTacToeController.CheckWin(new Vector2Int(0,2), NodeType.O);

			// Assert
			Assert.AreEqual(NodeType.None, winner);
		}

		[Test]
        public void CheckUndoMove()
        {
			// Arrange
			TurnController turnController = new TurnController();

			// Act
			var playerOne = new PlayerInput(turnController);
			var playerTwo = new PlayerInput(turnController);
			playerOne.SetNodeType(NodeType.X);
			playerTwo.SetNodeType(NodeType.O);
			turnController.PlayerOne = playerOne;
			turnController.PlayerTwo = playerTwo;
			turnController.StartGame();
			turnController.NodeMark(new Vector2Int(2, 2));
			turnController.NodeMark(new Vector2Int(2, 1));
			turnController.NodeMark(new Vector2Int(0, 2));
			turnController.NodeMark(new Vector2Int(0, 1));
			turnController.UndoMove();

			// Assert
			Assert.AreEqual(turnController.TicTacToeController.Board[0, 1].nodeType, NodeType.None);
		}

		[Test]
        public void CheckHint()
        {
			// Arrange
			TurnController turnController = new TurnController();

			// Act
			var playerOne = new PlayerInput(turnController);
			var playerTwo = new PlayerInput(turnController);
			playerOne.SetNodeType(NodeType.X);
			playerTwo.SetNodeType(NodeType.O);
			turnController.PlayerOne = playerOne;
			turnController.PlayerTwo = playerTwo;
			turnController.StartGame();
			var hintNode = turnController.GetNodeToHint();

			// Assert
			Assert.AreEqual(hintNode.nodeType, NodeType.None);
		}
    }
}
