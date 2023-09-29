using System;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeManager : BaseManager<TicTacToeManager>
{
    [SerializeField]
    private GridLayoutGroup tilesParent;
    [SerializeField]
    private RectTransform rectTransform;
    
	private TicTacToeController _ticTacToeController;
	public PlayerType CurrentPlayer => _ticTacToeController.CurrentPlayer;

	public void SpawnTiles()
    {
		_ticTacToeController = new TicTacToeController();

        var horizontalTilesCount = GameSettingsManager.Instance.Settings.HorizontalTilesCount;
		var verticalTilesCount = GameSettingsManager.Instance.Settings.VerticalTilesCount;
		_ticTacToeController._tileControllers = new TileController [horizontalTilesCount, verticalTilesCount];
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
                tile.Init(new Vector2(j, i), () => OnTilesButtonClick(tile));
                tile.gameObject.SetActive(true);
				_ticTacToeController._tileControllers[j, i] = tile;
            }
        }
	}

    private void OnTilesButtonClick(TileController tile)
    {
        tile.SetTileState(_ticTacToeController.CurrentPlayer);

        if (_ticTacToeController.CheckWin(tile) != PlayerType.None)
        {
            GameplayManager.Instance.SetGameOverState();
            EventsManager.Instance.OnGameOver(_ticTacToeController.CurrentPlayer);
            return;
        }

		_ticTacToeController.SwitchPlayer();
	}
}
