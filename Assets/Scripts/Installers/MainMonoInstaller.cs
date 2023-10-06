using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMonoInstaller : MonoInstaller
{
	[SerializeField]
	private BoardTurnController turnController;

	public override void InstallBindings()
	{
		Container.Bind<ITurnController>().FromInstance(turnController);
		Container.Bind<IBoardController>().To<BoardController>().AsSingle();
	}
}
