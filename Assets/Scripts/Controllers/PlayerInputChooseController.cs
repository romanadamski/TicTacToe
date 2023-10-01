public class PlayerInputChooseController : PlayerChooseController
{
	protected override IPlayer Player => new PlayerInput(nodeType, _turnManager);
}
