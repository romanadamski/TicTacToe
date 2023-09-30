using UnityEngine;
using UnityEngine.UI;

public class GameView : BaseManager<GameView>
{
	[SerializeField]
	private GridLayoutGroup tilesParent;
	[SerializeField]
	private RectTransform rectTransform;
	[SerializeField]
	private GameSettingsSO settings;

	//todo inject
	private TurnManager _turnManager => TurnManager.Instance;

	public TileController[,] TileControllers { get; private set; }

	public void SpawnTiles()
	{
		var horizontalTilesCount = settings.HorizontalTilesCount;
		var verticalTilesCount = settings.VerticalTilesCount;

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
				var tile = ObjectPoolingManager.Instance.GetFromPool("Tile").GetComponent<TileController>();
				tile.transform.SetParent(tilesParent.transform);
				tile.Init(new Vector2Int(j, i), () => OnTilesButtonClick(tile));
				tile.gameObject.SetActive(true);
				TileControllers[j, i] = tile;
			}
		}
	}

	private void OnTilesButtonClick(TileController tile)
	{
		_turnManager.CurrentPlayer.NodeMark(tile.Index);
	}

	public void OnNodeMark(Vector2Int index, NodeType nodeType)
	{
		var tile = TileControllers[index.x, index.y];
		tile.SetTileState(nodeType);
	}

	public void ToggleInput(bool toggle)
	{
		foreach(var item in TileControllers)
		{
			if (item.PlayerType != NodeType.None) continue;

			item.Button.interactable = toggle;
		}
	}
}
