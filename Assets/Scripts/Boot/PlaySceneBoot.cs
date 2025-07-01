using UnityEngine;
using Zenject;

public class PlaySceneBoot : MonoBehaviour
{
    [Inject] private BaseSceneServiceProvider _baseServiceProvider;
    [Inject] private GamePlayController _gamePlayController;
    [Inject] private CubeSpaner _cubeSpaner;
    [Inject] private BaseLevelData _levelData;
    [Inject] private FlexibleFactory _flexibleFactory;
    [Inject] private FlexibleFactoryPool _flexibleFactoryPool;

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

        _playSceneServiceProvider.Initialize(_gamePlayController, _cubeSpaner, _levelData, _flexibleFactory, _flexibleFactoryPool);
        _cubeSpaner.Initialize();
        _gamePlayController.Initialize();

    }

    private void StartGame()
    {
        _gamePlayController.StartGame();
    }
}
