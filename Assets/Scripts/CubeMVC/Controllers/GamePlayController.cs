using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GamePlayController : MonoBehaviour
{
    [Inject] private BaseSceneServiceProvider _sceneServiceProvider;
    private PlaySceneServiceProvider _playSceneServiceProvider;
    private CubeSpaner _cubeSpaner => _playSceneServiceProvider.CubeSpaner;
    private InputController _inputController => _playSceneServiceProvider.InputController;

    private const int _gameIterationDelay = 2000;

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

    public async void GoNextGameIteration()
    {
        await Task.Delay(_gameIterationDelay);

        _cubeSpaner.SpawnCube();
        _inputController.ChangeState(InputState.InputActive);
    }

    public void StartGame()
    {
        _cubeSpaner.SpawnCube();
        _inputController.ChangeState(InputState.InputActive);
    }
}
