using System;

[Serializable]
public class SaveData
{
    public float verticalNodes;
    public float horizontalNodes;
    public float winningNodes;
    public float playerTurnTimeLimit;

    public SaveData()
    {
        verticalNodes = 3;
        horizontalNodes = 3;
        winningNodes = 3;
        playerTurnTimeLimit = 5;
    }
}
