using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerInput : SetPlayerBaseChoice
{
    protected override IPlayer Player => new PlayerInput(_turnController);
}
