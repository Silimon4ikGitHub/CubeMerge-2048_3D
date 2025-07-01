using UnityEngine;

public class TemporaryInfo
{
    public BaseLevelData CurrentLevelData {  get; private set; }

    public void ChangeLevel(BaseLevelData levelData)
    {
        CurrentLevelData = levelData;
    }
}
