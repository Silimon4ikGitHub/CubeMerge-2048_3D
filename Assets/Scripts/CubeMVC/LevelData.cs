using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Levels/New Level Data")]
public class LevelData : BaseLevelData
{
    public int LevelCompleteScore => _levelCompleteScore;
    public LevelData NextLevel => _nextLevel;

    [SerializeField] private int _levelCompleteScore;
    [SerializeField] private LevelData _nextLevel;
}
