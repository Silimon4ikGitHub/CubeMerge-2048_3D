using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Levels/New Level Data")]
public class LevelData : BaseLevelData
{
    public int LevelCompleteScore => _levelCompleteScore;

    [SerializeField] private int _levelCompleteScore;
}
