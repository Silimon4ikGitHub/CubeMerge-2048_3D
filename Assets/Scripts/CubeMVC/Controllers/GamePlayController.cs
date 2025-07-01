using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GamePlayController : MonoBehaviour
{
    [Inject] private BaseSceneServiceProvider _sceneServiceProvider;
    private PlaySceneServiceProvider _playSceneServiceProvider;
    private CubeSpaner _cubeSpaner => _playSceneServiceProvider.CubeSpaner;
    private InputController _inputController => _playSceneServiceProvider.InputController;

    private const int _gameIterationDelay = 1500;

    public void Initialize()
    {
        if (_sceneServiceProvider is PlaySceneServiceProvider playSceneServiceProvider)
        {
            _playSceneServiceProvider = playSceneServiceProvider;
        }
        else
        {
            Debug.LogError("GamePlayController not initialized, check DI");
        }
    }

    public async void GoNextGameIteration()
    {
        await Task.Delay(_gameIterationDelay);

        _cubeSpaner.SpawnCubeByChance();
        _inputController.ChangeState(InputState.InputActive);
    }

    public void StartGame()
    {
        _cubeSpaner.SpawnCubeByChance();
        _inputController.ChangeState(InputState.InputActive);
    }

    public void UpgradeElement(BaseGamePlayElementView element)
    {
        var elementModel = element.GetModel();

        if (elementModel is not CubeModel cubeModel || cubeModel.UpgradeModel == null)
        {
            Debug.Log("The Element " + elementModel.name.ToString() + " Not have a next type");
            return;
        }

        var position = element.transform.position;
        Vector3 savedVelocity = element.Rigidbody.linearVelocity;
        Vector3 savedAngularVelocity = element.Rigidbody.angularVelocity;

        element.OnDespawn();

        var newCube = _cubeSpaner.SpawnCube(cubeModel.UpgradeModel, position);

        if (newCube is CubeView cubeView)
        {
            cubeView.OnUpgradeSpawn();
            cubeView.Rigidbody.linearVelocity = savedVelocity;
            cubeView.Rigidbody.angularVelocity = savedAngularVelocity;
        }
    }
}
