using UnityEngine;
using Zenject;

public class GamePlayController : MonoBehaviour
{
    [Inject] private BaseSceneServiceProvider _sceneServiceProvider;
    private PlaySceneServiceProvider _playSceneServiceProvider;
    private CubeSpaner _cubeSpaner => _playSceneServiceProvider.CubeSpaner;

    public void Initialize()
    {
        if (_sceneServiceProvider is PlaySceneServiceProvider playSceneServiceProvider)
        {
            _playSceneServiceProvider = playSceneServiceProvider;
        }
        else
        {
            Debug.Log("GamePlayController not initialized, check DI");
        }
    }

    public void StartGame()
    {
        _cubeSpaner.SpawnCube();
    }
}
