using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMonoInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<TurnController>().AsSingle();
	}
}
