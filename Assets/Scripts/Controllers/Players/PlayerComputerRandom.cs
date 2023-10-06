using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerComputerRandom : IPlayer
{
	[SerializeField]
	private BoardEventsSO boardEventsSO;

	private float RandomTimeInterval => Random.Range(0.5f, 3.0f);
	private Coroutine _turnCoroutine;

	public override bool AllowInput => false;

	[Inject]
	private IBoardController _boardController;

	public override void OnStartTurn()
	{
		base.OnStartTurn();
		StopTurnCoroutine();
		Invoke(nameof(MakeMove), RandomTimeInterval);
	}

	private IEnumerator WaitAndTakeTurn()
    {
        yield return new WaitForSeconds(RandomTimeInterval);

        MakeMove();
    }

    private void MakeMove()
    {
        var index = _boardController.GetRandomEmptyNode().index;
        boardEventsSO.SetNode(this, index);
    }

    private void StopTurnCoroutine()
	{
		CancelInvoke(nameof(MakeMove));
		if(_turnCoroutine != null)
		{
			GameManager.Instance.StopCoroutine(_turnCoroutine);
		}
		_turnCoroutine = null;
	}

	public override void OnTurnEnd()
	{
		base.OnTurnEnd();

		StopTurnCoroutine();
	}
}
