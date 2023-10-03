using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/*
public class TurnControllerMono : MonoBehaviour
{
	[SerializeField]
	private SettingsSO settingsSO;

	private uint HorizontalTilesCount => settingsSO.HorizontalNodes;
	private uint VerticalTilesCount => settingsSO.VerticalNodes;
	private uint WinningTilesCount => settingsSO.WinningNodes;
	private float PlayerTurnTimeLimit => settingsSO.PlayerTurnTimeLimit;
	private Coroutine _turnEndCoroutine;
	private float _turnElapsed;
	public float TurnElapsed
	{
		get => _turnElapsed;
		set
		{
			_turnElapsed = value;
			OnTimerChanged?.Invoke(value);
		}
	}


	private void StartTurn(IPlayer player)
	{
		StopTurnEndCoroutine();
		_turnEndCoroutine = GameManager.Instance.StartCoroutine(TimerCoroutine(player));
		player.OnStartTurn();
	}

	private void ResetTurnTime()
	{
		TurnElapsed = PlayerTurnTimeLimit;
	}

	private void StopTurnEndCoroutine()
	{
		if (_turnEndCoroutine != null)
		{
			GameManager.Instance.StopCoroutine(_turnEndCoroutine);
		}
		ResetTurnTime();
	}

	private IEnumerator TimerCoroutine(IPlayer player)
	{
		ResetTurnTime();
		while (TurnElapsed > 0)
		{
			TurnElapsed -= Time.deltaTime;
			yield return null;
		}
		SetLoser(player);
	}

}
*/