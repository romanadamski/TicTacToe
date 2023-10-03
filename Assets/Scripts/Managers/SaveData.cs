using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public float verticalNodes;
    public float horizontalNodes;
    public float winningNodes;

    public SaveData()
    {
        verticalNodes = 3;
        horizontalNodes = 3;
        winningNodes = 3;
    }
}
