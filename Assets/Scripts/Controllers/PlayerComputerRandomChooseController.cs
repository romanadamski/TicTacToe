public class PlayerComputerRandomChooseController : PlayerChooseController
{
	protected override IPlayer Player => new PlayerComputerRandom(nodeType, _turnManager);
}
