using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Levels/New Level Data")]
public class LevelData : BaseLevelData
{
    [SerializeField] private int _levelCompleteScore;
}
