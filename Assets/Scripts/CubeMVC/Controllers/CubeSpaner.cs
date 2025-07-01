using System.Linq;
using UnityEngine;
using Zenject;

public class CubeSpaner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _container;

    [Inject] private BaseSceneServiceProvider _sceneServiceProvider;
    
    private PlaySceneServiceProvider _playSceneServiceProvider;
    private FlexibleFactory _factory => _playSceneServiceProvider.Factory;
    private BaseLevelData _levelData => _playSceneServiceProvider.LevelData;

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

    public void SpawnCube()
    {
        var randomCubeModel = GetCubeModelByChance();

        Debug.Log("Model = " + (randomCubeModel == null).ToString());
        Debug.Log("Prefab = " + (randomCubeModel.Prefab == null).ToString());

        if (randomCubeModel.Prefab != null)
        {
            var clampedPos = _spawnPoint.position;

            var element = _factory.Create(randomCubeModel.Prefab.gameObject, clampedPos, _container);

            if (element is BaseGamePlayElementView playElement)
            {
                playElement.Setup(randomCubeModel);
                _currentElement = playElement;
                //_isControllerActive = true;
            }
        }
        else
        {
            Debug.LogWarning($"No prefab found for Model {randomCubeModel.name}");
        }
    }

    private BaseElementModel GetCubeModelByChance()
    {
        float roll = Random.Range(0.1f, 100);
        float cumulative = 0;
        var models = _levelData.ElementsToSpawn;

        foreach (var result in models)
        {
            cumulative += result.SpanChancePercent;

            if (roll <= cumulative)
            {
                Debug.Log("Model = " + (result.Model == null).ToString());
                Debug.Log("Prefab = " + (result.Model.Prefab == null).ToString());
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
