using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class FlexibleFactoryPool
{
    private readonly Dictionary<FactoryElementType, List<IFactoryElement>> _pool = new();
    private readonly Dictionary<int, IFactoryElement> _activeElements = new();

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

    public async Task<IFactoryElement> GetOrCreateAsync(IFactoryElementModel reference, Vector3 position, Transform parent = null)
    {
        List<IFactoryElement> typePool;
        IFactoryElement instance = null;

        _pool.TryGetValue(reference.Type, out var existPool);
        typePool = existPool;
        
        if (typePool != null)
        {
            foreach (var element in typePool)
            {
                if (element.Active == false)
                {
                    instance = element;
                }
            }
        }
        else
        {
            _pool.Add(reference.Type, new List<IFactoryElement>());
        }

        if (instance == null || instance.Active)
        {
            var handle = reference.Prefab.InstantiateAsync(parent);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to instantiate prefab from reference: {reference}");
                return null;
            }

            instance = handle.Result.GetComponent<IFactoryElement>();

            if (instance == null)
            {
                Debug.LogError("Prefab does not implement IFabricElement.");
                return null;
            }

            instance.Initialize(_projectServices, _sceneServices);
            _pool.TryGetValue(reference.Type, out var objectPool);
            objectPool.Add(instance);
        }

        instance.Id = _idCounter++;
        instance.OnSpawn(position, parent);
        return instance;
    }

    public void Return(IFactoryElementModel model, IFactoryElement element)
    {
        element.OnDespawn();
    }

    public IFactoryElement GetById(int id)
    {
        _activeElements.TryGetValue(id, out var element);
        return element;
    }

    public List<IFactoryElement> GetAllActive()
    {
        return _activeElements.Values
            .Where(e => (e as MonoBehaviour)?.gameObject.activeSelf == true)
            .ToList();
    }
}
