using UnityEngine;

public class FlexibleFactory
{
    private readonly FlexibleFactoryPool _pool;

    public FlexibleFactory(FlexibleFactoryPool pool)
    {
        _pool = pool;
    }

    public IFabricElement Create(GameObject prefab, Vector3 position, Transform parent = null)
    {
        return _pool.GetOrCreate(prefab, position, parent);
    }

    public void Despawn(GameObject prefab, IFabricElement element)
    {
        _pool.Return(prefab, element);
    }
}