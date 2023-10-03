using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SetPlayerComputerRandom : SetPlayerBaseChoice
{
    protected override IPlayer Player => new PlayerComputerRandom(_turnController);
}