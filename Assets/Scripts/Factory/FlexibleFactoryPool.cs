using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class FlexibleFactoryPool
{
    private readonly Dictionary<GameObject, Stack<IFabricElement>> _pool = new();
    private readonly Dictionary<int, IFabricElement> _activeElements = new();

    private readonly DiContainer _container;
    private readonly BaseSceneServiceProvider _sceneServices;

    private int _idCounter;

    public FlexibleFactoryPool(DiContainer container, BaseSceneServiceProvider sceneServices)
    {
        _container = container;
        _sceneServices = sceneServices;
    }

    public IFabricElement GetOrCreate(GameObject prefab, Vector3 position, Transform parent = null)
    {
        IFabricElement instance;

        if (!_pool.TryGetValue(prefab, out var stack) || stack.Count == 0)
        {
            instance = _container.InstantiatePrefabForComponent<IFabricElement>(prefab);
            instance.Initialize(_sceneServices);
        }
        else
        {
            instance = stack.Pop();
        }

        instance.Id = _idCounter;
        _idCounter++;

        instance.OnSpawn(position, parent);
        return instance;
    }

    public void Return(GameObject prefab, IFabricElement element)
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
