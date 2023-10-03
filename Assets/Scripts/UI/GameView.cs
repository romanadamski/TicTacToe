using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(ObjectPoolingController))]
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
	[SerializeField]
	private SettingsSO settingsSO;

	private ObjectPoolingController _objectPoolingController;
	private TileController _currentHighlightTile;

	[Inject]
	private TurnController _turnController;

	public TileController[,] TileControllers { get; private set; }

	private void Awake()
	{
		_objectPoolingController = GetComponent<ObjectPoolingController>();

		_turnController.OnSetNode += OnNodeMark;
		_turnController.OnPlayerChanged += ToggleInput;
		_turnController.OnGameplayStarted += SpawnBoard;
		_turnController.OnGameplayFinished += ClearBoard;
		_turnController.OnHint += OnHint;
	}

	private void OnHint(Vector2Int index, NodeType nodeType)
	{
		if(_currentHighlightTile != null)
		{
			_currentHighlightTile.EndHighlightCoroutine();
		}

		_currentHighlightTile = TileControllers[index.x, index.y];
		_currentHighlightTile.Highlight(nodeType);
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
		var horizontalTilesCount = settingsSO.HorizontalNodes;
		var verticalTilesCount = settingsSO.VerticalNodes;

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

	private void OnTilesButtonClick(TileController tile)
	{
		_turnController.NodeMark(tile.Index);
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
