using UnityEngine;
using Zenject;

public class FinishGameController : MonoBehaviour
{
    [SerializeField] private LoseLimit _loseLimit;

    [Inject] private BaseSceneServiceProvider _sceneServiceProvider;
    [Inject] private ProjectServiceProvider _projectServiceProvider;
    private TemporaryInfo _temporaryInfo => _projectServiceProvider.TemporaryInfo;
    private PlaySceneServiceProvider _playSceneServiceProvider;
    private PlaySceneUI _playSceneUI => _playSceneServiceProvider.PlaySceneUI;
    private GamePlayController _gamePlayController => _playSceneServiceProvider.GameplayController;
    private LevelData _cubeLevelData;
    private ScoreHolder _scoreHolder;

    public void Init()
    {
        CheckDependencies();
        _scoreHolder = new ScoreHolder();

        OnScoreChange(0);

        _loseLimit.Setup(_gamePlayController, this);
        _scoreHolder.OnScoreChanged += OnScoreChange;
    }

    public void CalculateScore(CubeView mainCube)
    {
        var mainCubeValue = mainCube.Value;

        int result = (int)(mainCubeValue / 2);

        _scoreHolder.AddScore(result);
    }

    public void OnGameLose()
    {
        _playSceneUI.ShowGameLose();
    }

    private void CheckDependencies()
    {
        if (_sceneServiceProvider is PlaySceneServiceProvider playSceneServiceProvider)
        {
            _playSceneServiceProvider = playSceneServiceProvider;
        }
        else
        {
            Debug.LogError("FinishGameController not initialized, check DI");
        }

        if (_temporaryInfo.CurrentLevelData is LevelData cubelevelData)
        {
            _cubeLevelData = cubelevelData;
        }
        else
        {
            Debug.LogError("FinishGameController not initialized, check DI");
        }
    }

    private void OnScoreChange(int newScore)
    {
        _playSceneUI.UpdateScore(newScore);

        if (newScore >= _cubeLevelData.LevelCompleteScore)
        {
            OnGameWin();
        }
    }

    private void OnGameWin()
    {
        _playSceneUI.ShowGameWin();
    }
}
