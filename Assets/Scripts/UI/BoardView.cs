using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(ObjectPoolingController))]
public class BoardView : MonoBehaviour
{
	[SerializeField]
	private GridLayoutGroup tilesParent;
	[SerializeField]
	private RectTransform gridRectTransform;
	[SerializeField]
	private GridLayoutGroup verticalLines;
	[SerializeField]
	private GridLayoutGroup horizontalLines;
	[SerializeField]
	private SettingsSO settingsSO;
	[SerializeField]
	private GameplayEventsSO gameplayEventsSO;
	[SerializeField]
	private BoardEventsSO boardEventsSO;
	[SerializeField]
	private TurnEventsSO turnEventsSO;

	private ObjectPoolingController _objectPoolingController;
	private TileController _currentHighlightTile;

	private IBoardController _boardController;
	private TileController[,] _tileControllers;

	[Inject]
	private ITurnController _turnController;

	private void Awake()
    {
        _objectPoolingController = GetComponent<ObjectPoolingController>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
		boardEventsSO.OnSetNode += OnSetNode;
		boardEventsSO.OnSetNode += TryEndGame;
		turnEventsSO.OnPlayerChanged += ToggleInput;
		gameplayEventsSO.OnGameplayStarted += StartGame;
		gameplayEventsSO.OnGameplayFinished += ClearBoard;
		boardEventsSO.OnHint += OnHint;
        boardEventsSO.OnUndoMove += UndoMove;
    }

    private void UndoMove()
    {
		var undoIsPossible = _boardController.TryUndoMove(out var lastMove);
		if (undoIsPossible)
        {
			SetNodeUI(NodeType.None, lastMove.Item2);
			_turnController.UndoTurn();
		}
	}

    private void OnHint()
	{
		var randomNode = _boardController.GetRandomEmptyNode();

		if (_currentHighlightTile != null)
		{
			_currentHighlightTile.EndHighlightCoroutine();
		}

		_currentHighlightTile = _tileControllers[randomNode.index.x, randomNode.index.y];
		_currentHighlightTile.Highlight(randomNode.nodeType);
	}

	public void ClearBoard()
    {
		_objectPoolingController.ReturnAllToPools();
	}

	public void StartGame()
    {
		_boardController = new BoardController(settingsSO.HorizontalNodes, settingsSO.VerticalNodes, settingsSO.WinningNodes);

		SpawnBoard();
    }

    private void SpawnBoard()
    {
        SpawnTiles();
        SpawnLines();
    }

    private void SpawnTiles()
	{
		var horizontalTilesCount = settingsSO.HorizontalNodes;
		var verticalTilesCount = settingsSO.VerticalNodes;

		_tileControllers = new TileController[horizontalTilesCount, verticalTilesCount];
		tilesParent.constraintCount = (int)horizontalTilesCount;
		var cellSize = horizontalTilesCount > verticalTilesCount
			? gridRectTransform.rect.width / horizontalTilesCount
			: gridRectTransform.rect.height / verticalTilesCount;
		tilesParent.cellSize = new Vector2(cellSize, cellSize);

		for (int i = 0; i < verticalTilesCount; i++)
		{
			for (int j = 0; j < horizontalTilesCount; j++)
			{
				var tile = _objectPoolingController.GetFromPool("Tile").GetComponent<TileController>();
				tile.transform.SetParent(tilesParent.transform);
				tile.Init(new Vector2Int(j, i), () => OnTilesButtonClick(tile));
				tile.gameObject.SetActive(true);
				_tileControllers[j, i] = tile;
			}
		}
	}

	private void SpawnLines()
	{
		for (int i = 0; i < settingsSO.HorizontalNodes - 1; i++)
		{
			var line = _objectPoolingController.GetFromPool("LineVertical");

			line.GetComponent<RectTransform>().sizeDelta = new Vector2(43, settingsSO.VerticalNodes * tilesParent.cellSize.y);
			line.gameObject.SetActive(true);
		}

		var lineSize = new Vector2(43, settingsSO.VerticalNodes * tilesParent.cellSize.y);

		verticalLines.cellSize = lineSize;
		verticalLines.spacing = new Vector2(tilesParent.cellSize.y - 43, 0);

		lineSize = new Vector2(settingsSO.HorizontalNodes * tilesParent.cellSize.x, 43);
		for (int i = 0; i < settingsSO.VerticalNodes - 1; i++)
		{
			var line = _objectPoolingController.GetFromPool("LineHorizontal");
			line.GetComponent<RectTransform>().sizeDelta = lineSize;
			line.gameObject.SetActive(true);
		}
		horizontalLines.cellSize = lineSize;
		horizontalLines.spacing = new Vector2(0, tilesParent.cellSize.x -43);
	}

	//todo refactor get player
	private void OnTilesButtonClick(TileController tile)
	{
		boardEventsSO.SetNode(_turnController.CurrentPlayer, tile.Index);
	}

	//todo refactor call in playerrandomcomputer
	private void OnSetNode(IPlayer player, Vector2Int index)
    {
        SetNodeUI(player.NodeType, index);
        _boardController.SetNode(index, player.NodeType);
        _boardController.SaveMove(player, index);
    }

    private void SetNodeUI(NodeType nodeType, Vector2Int index)
    {
        var tile = _tileControllers[index.x, index.y];
        tile.SetState(nodeType);
    }

    private void TryEndGame(IPlayer player, Vector2Int index)
	{
		var winner = _boardController.CheckWin(index, player.NodeType);
		if (winner != NodeType.None)
		{
			gameplayEventsSO.GameOver(player);
		}
		else if (!_boardController.CheckEmptyNodes())
		{
			gameplayEventsSO.GameOver(null);
		}
	}

	public void ToggleInput(IPlayer player)
	{
		if (_tileControllers == null) return;

		foreach(var item in _tileControllers)
		{
			if (item.NodeType != NodeType.None) continue;

			item.Button.interactable = player.AllowInput;
		}
	}
}
