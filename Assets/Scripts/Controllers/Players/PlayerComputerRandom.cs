using UnityEngine;
using Zenject;

public class PlayerComputerRandom : IPlayer
{
	[SerializeField]
	private BoardEventsSO boardEventsSO;

	private float RandomTimeInterval => Random.Range(0.5f, 3.0f);

	public override bool AllowInput => false;

	[Inject]
	private IBoardController _boardController;

	public override void OnStartTurn()
	{
		base.OnStartTurn();
		Invoke(nameof(MakeMove), RandomTimeInterval);
	}

    private void MakeMove()
    {
        var index = _boardController.GetRandomEmptyNode().index;
        boardEventsSO.SetNode(this, index);
    }

    private void StopInvokingMove()
	{
		CancelInvoke(nameof(MakeMove));
	}

	public override void OnTurnEnd()
	{
		base.OnTurnEnd();

		StopInvokingMove();
	}
}
