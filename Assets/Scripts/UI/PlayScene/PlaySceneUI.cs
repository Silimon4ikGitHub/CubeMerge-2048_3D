using TMPro;
using UnityEngine;

public class PlaySceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameResultWindow _winWindow;
    [SerializeField] private GameResultWindow _loseWindow;

    public void Initialize()
    {
        _winWindow.Initialize();
        _loseWindow.Initialize();
    }

    public void UpdateScore(int value)
    {
        _scoreText.text = value.ToString();
    }

    public void ShowGameWin()
    {
        _winWindow.gameObject.SetActive(true);
    }

    public void ShowGameLose()
    {
        _loseWindow.gameObject.SetActive(true);
    }
}
