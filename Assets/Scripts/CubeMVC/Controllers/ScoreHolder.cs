using System;
using UnityEngine;

public class ScoreHolder
{
    public int Score {  get; private set; }

    public event Action<int> OnScoreChanged;

    public void AddScore(int value)
    {
        Score += value;

        OnScoreChanged?.Invoke(Score);
    }
}
