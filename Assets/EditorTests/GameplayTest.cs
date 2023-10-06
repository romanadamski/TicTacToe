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
			_ticTacToeController = new BoardController();
			_ticTacToeController.Set(3,3,3);
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
			IBoardController boardController = new BoardController();
			boardController.Set(3, 3, 3);
			var playerOneObject = new GameObject();
			var playerTwoObject = new GameObject();
			var playerOne = playerOneObject.AddComponent<PlayerInput>();
			var playerTwo = playerTwoObject.AddComponent<PlayerInput>();

			playerOne.SetNodeType(NodeType.X);
			playerTwo.SetNodeType(NodeType.O);

			// Act
			boardController.SetNode(new Vector2Int(2, 2), playerOne.NodeType);
			boardController.SaveMove(playerOne, new Vector2Int(2, 1));
			boardController.SetNode(new Vector2Int(2, 1), playerTwo.NodeType);
			boardController.SaveMove(playerTwo, new Vector2Int(2, 1));
			boardController.SetNode(new Vector2Int(0, 2), playerOne.NodeType);
			boardController.SaveMove(playerOne, new Vector2Int(2, 1));
			boardController.SetNode(new Vector2Int(0, 1), playerTwo.NodeType);
			boardController.SaveMove(playerTwo, new Vector2Int(2, 1));
			var undo = boardController.TryUndoMove(out var node);

			// Assert
			Assert.IsTrue(undo);
            Assert.AreEqual(node.Item1.NodeType, playerTwo.NodeType);
        }

		[Test]
        public void CheckHint()
        {
			// Arrange
			IBoardController boardController = new BoardController();
			boardController.Set(3, 3, 3);

			// Act
			var randomEmptyNode = boardController.GetRandomEmptyNode();

			// Assert
			Assert.IsTrue(randomEmptyNode.index.x < 3 && randomEmptyNode.index.x >= 0);
			Assert.IsTrue(randomEmptyNode.index.y < 3 && randomEmptyNode.index.y >= 0);
		}
    }
}
