using UnityEngine;
using Zenject;

public class PlaySceneBoot : MonoBehaviour
{
    [Inject] private BaseSceneServiceProvider _baseServiceProvider;
    [Inject] private GamePlayController _gamePlayController;
    [Inject] private CubeSpaner _cubeSpaner;
    [Inject] private FlexibleFactory _flexibleFactory;
    [Inject] private FlexibleFactoryPool _flexibleFactoryPool;
    [Inject] private InputController _inputController;
    [Inject] private FinishGameController _finishGameController;
    [Inject] private PlaySceneUI _playSceneUI;

    private PlaySceneServiceProvider _playSceneServiceProvider;

    public void Start()
    {
        InitializeDependencies();

        StartGame();
    }

    private void InitializeDependencies()
    {
        if (_baseServiceProvider is PlaySceneServiceProvider playSceneServices)
        {
            _playSceneServiceProvider = playSceneServices;
        }
        else
        {
            Debug.Log("No Services loaded on boot");

            return;
        }

        _playSceneServiceProvider.Initialize(_gamePlayController, _cubeSpaner, _flexibleFactory, _flexibleFactoryPool, _inputController, _finishGameController, _playSceneUI);
        _cubeSpaner.Initialize();
        _gamePlayController.Initialize();
        _inputController.Initialize();
        _finishGameController.Init();
        _playSceneUI.Initialize();
    }

    private void StartGame()
    {
        _gamePlayController.StartGame();
    }
}
