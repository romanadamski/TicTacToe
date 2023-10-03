using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameView : MonoBehaviour
{
	[SerializeField]
	private GridLayoutGroup tilesParent;
	[SerializeField]
	private RectTransform rectTransform;
	[SerializeField]
	private GridLayoutGroup verticalLines;
	[SerializeField]
	private GridLayoutGroup horizontalLines;

	private ObjectPoolingController _objectPoolingController;

	[Inject]
	private TurnManager _turnManager;
	private TileController _currentHighlightTile;

	public TileController[,] TileControllers { get; private set; }

	private void Awake()
	{
		_objectPoolingController = GetComponent<ObjectPoolingController>();

		EventsManager.Instance.NodeMark += OnNodeMark;
		EventsManager.Instance.PlayerChanged += ToggleInput;
		EventsManager.Instance.GameplayStarted += SpawnBoard;
		EventsManager.Instance.GameplayFinished += ClearBoard;
		EventsManager.Instance.Hint += OnHint;
	}

	private void OnHint(Vector2Int index)
	{
		if(_currentHighlightTile != null)
		{
			_currentHighlightTile.EndHighlightCoroutine();
		}

		_currentHighlightTile = TileControllers[index.x, index.y];
		_currentHighlightTile.Highlight(_turnManager.CurrentPlayer.NodeType);
	}

	public void ClearBoard()
    {
		_objectPoolingController.ReturnAllToPools();

	}

	public void SpawnBoard()
	{
		SpawnTiles();
		SpawnLines();
	}

	private void SpawnTiles()
	{
		var horizontalTilesCount = _turnManager.HorizontalTilesCount;
		var verticalTilesCount = _turnManager.VerticalTilesCount;

		TileControllers = new TileController[horizontalTilesCount, verticalTilesCount];
		tilesParent.constraintCount = (int)horizontalTilesCount;
		var cellSize = horizontalTilesCount > verticalTilesCount
			? rectTransform.rect.width / horizontalTilesCount
			: rectTransform.rect.height / verticalTilesCount;
		tilesParent.cellSize = new Vector2(cellSize, cellSize);

		for (int i = 0; i < verticalTilesCount; i++)
		{
			for (int j = 0; j < horizontalTilesCount; j++)
			{
				var tile = _objectPoolingController.GetFromPool("Tile").GetComponent<TileController>();
				tile.transform.SetParent(tilesParent.transform);
				tile.Init(new Vector2Int(j, i), () => OnTilesButtonClick(tile));
				tile.gameObject.SetActive(true);
				TileControllers[j, i] = tile;
			}
		}
	}

	private void SpawnLines()
	{
		for (int i = 0; i < _turnManager.HorizontalTilesCount - 1; i++)
		{
			var line = _objectPoolingController.GetFromPool("LineVertical");

			line.GetComponent<RectTransform>().sizeDelta = new Vector2(43, _turnManager.VerticalTilesCount * tilesParent.cellSize.y);
			line.gameObject.SetActive(true);
		}

		var lineSize = new Vector2(43, _turnManager.VerticalTilesCount * tilesParent.cellSize.y);

		verticalLines.cellSize = lineSize;
		verticalLines.spacing = new Vector2(tilesParent.cellSize.y - 43, 0);

		lineSize = new Vector2(_turnManager.HorizontalTilesCount * tilesParent.cellSize.x, 43);
		for (int i = 0; i < _turnManager.VerticalTilesCount - 1; i++)
		{
			var line = _objectPoolingController.GetFromPool("LineHorizontal");
			line.GetComponent<RectTransform>().sizeDelta = lineSize;
			line.gameObject.SetActive(true);
		}
		horizontalLines.cellSize = lineSize;
		horizontalLines.spacing = new Vector2(0, tilesParent.cellSize.x -43);
	}

	private void OnTilesButtonClick(TileController tile)
	{
		_turnManager.NodeMark(tile.Index);
	}

	private void OnNodeMark(Vector2Int index, NodeType nodeType)
	{
		var tile = TileControllers[index.x, index.y];
		tile.SetState(nodeType);
	}

	public void ToggleInput(IPlayer player)
	{
		foreach(var item in TileControllers)
		{
			if (item.NodeType != NodeType.None) continue;

			item.Button.interactable = player.AllowInput;
		}
	}
}
