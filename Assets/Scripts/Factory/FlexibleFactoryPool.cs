using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class FlexibleFactoryPool
{
    private readonly Dictionary<AssetReference, Stack<IFabricElement>> _pool = new();
    private readonly Dictionary<int, IFabricElement> _activeElements = new();

    private readonly DiContainer _container;
    private readonly BaseSceneServiceProvider _sceneServices;
    private readonly ProjectServiceProvider _projectServices;

    private int _idCounter;

    public FlexibleFactoryPool(DiContainer container, ProjectServiceProvider projectServiceProvider, BaseSceneServiceProvider sceneServices)
    {
        _projectServices = projectServiceProvider;
        _container = container;
        _sceneServices = sceneServices;
    }

    public async Task<IFabricElement> GetOrCreateAsync(AssetReference reference, Vector3 position, Transform parent = null)
    {
        IFabricElement instance;

        if (!_pool.TryGetValue(reference, out var stack) || stack.Count == 0)
        {
            var handle = reference.InstantiateAsync(parent);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to instantiate prefab from reference: {reference}");
                return null;
            }

            instance = handle.Result.GetComponent<IFabricElement>();
            if (instance == null)
            {
                Debug.LogError("Prefab does not implement IFabricElement.");
                return null;
            }

            instance.Initialize(_projectServices, _sceneServices);
        }
        else
        {
            instance = stack.Pop();
        }

        instance.Id = _idCounter++;
        instance.OnSpawn(position, parent);
        return instance;
    }

    public void Return(AssetReference prefab, IFabricElement element)
    {
        element.OnDespawn();

        if (!_pool.ContainsKey(prefab))
        {
            _pool[prefab] = new Stack<IFabricElement>();
        }

        _pool[prefab].Push(element);
    }

    public IFabricElement GetById(int id)
    {
        _activeElements.TryGetValue(id, out var element);
        return element;
    }

    public List<IFabricElement> GetAllActive()
    {
        return _activeElements.Values
            .Where(e => (e as MonoBehaviour)?.gameObject.activeSelf == true)
            .ToList();
    }
}
