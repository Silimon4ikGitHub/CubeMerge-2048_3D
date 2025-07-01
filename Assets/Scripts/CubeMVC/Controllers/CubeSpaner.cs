using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CubeSpaner : MonoBehaviour
{
    public BaseGamePlayElementView CurrentElement => _currentElement;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _container;

    [Inject] private ProjectServiceProvider _projectServiceProvider;
    [Inject] private BaseSceneServiceProvider _sceneServiceProvider;
    
    private PlaySceneServiceProvider _playSceneServiceProvider;
    private FlexibleFactory _factory => _playSceneServiceProvider.Factory;
    private TemporaryInfo _temporaryInfo => _projectServiceProvider.TemporaryInfo;
    private BaseGamePlayElementView _currentElement;

    public void Initialize()
    {
        if (_sceneServiceProvider is PlaySceneServiceProvider playSceneServiceProvider)
        {
            _playSceneServiceProvider = playSceneServiceProvider;
        }
        else
        {
            Debug.Log("CubeSpaner not initialized, check DI");
        }
    }

    public async void SpawnCubeByChance()
    {
        var randomCubeModel = GetCubeModelByChance();

        if (randomCubeModel.Prefab == null && _spawnPoint != null)
        {
            Debug.LogWarning($"No prefab found for Model {randomCubeModel.name}");
            return;
        }

        var clampedPos = _spawnPoint.position;

        var newElement = await SpawnCube(randomCubeModel, clampedPos);

        if (newElement is BaseGamePlayElementView playElement)
        {
            _currentElement = playElement;
        }
    }

    public async Task<IFabricElement> SpawnCube(BaseElementModel model, Vector3 position)
    {
        if (model.Prefab != null)
        {
            var element = await _factory.Create(model.Prefab, position, _container);

            if (element is BaseGamePlayElementView playElement)
            {
                playElement.Setup(model);
            }

            return element;
        }
        else
        {
            Debug.LogWarning($"No prefab found for Model {model.name}");

            return null;
        }
    }

    private BaseElementModel GetCubeModelByChance()
    {
        float roll = Random.Range(0.1f, 100);
        float cumulative = 0;
        var models = _temporaryInfo.CurrentLevelData.ElementsToSpawn;

        foreach (var result in models)
        {
            cumulative += result.SpanChancePercent;

            if (roll <= cumulative)
            {
                return result.Model;
            }
        }

        return models.FirstOrDefault().Model;
    }

    private Vector3 GetRandomPosition()
    {
        if (_spawnPoint == null)
        {
            Debug.LogWarning("SpawnerSource is not assigned!");
            return Vector3.zero;
        }

        var bounds = GetWorldBounds(_spawnPoint);

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        Debug.Log("RESULT = " + new Vector3(x, y, z).ToString());

        return new Vector3(x, y, z);
    }

    private Bounds GetWorldBounds(Transform t)
    {
        var scale = t.lossyScale;
        var center = t.position;
        var size = Vector3.Scale(Vector3.one, scale);
        return new Bounds(center, size);
    }
}
