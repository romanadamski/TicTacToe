using UnityEngine;
using UnityEngine.UI;

public class GameView : BaseManager<GameView>
{
	[SerializeField]
	private GridLayoutGroup tilesParent;
	[SerializeField]
	private RectTransform rectTransform;
	[SerializeField]
	private GameSettingsScriptableObject settings;

	private TileController[,] _tileControllers;
	//todo inject
	private TurnManager _turnManager => TurnManager.Instance;

	public void SpawnTiles()
	{
		var horizontalTilesCount = settings.HorizontalTilesCount;
		var verticalTilesCount = settings.VerticalTilesCount;

		_tileControllers = new TileController[horizontalTilesCount, verticalTilesCount];
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
				_tileControllers[j, i] = tile;
			}
		}
	}

	private void OnTilesButtonClick(TileController tile)
	{
		tile.SetTileState(_turnManager.CurrentPlayer.Type);
		_turnManager.OnNodeMark(tile.Index);
	}
}
