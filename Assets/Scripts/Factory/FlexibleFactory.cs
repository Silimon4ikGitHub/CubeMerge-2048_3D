using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FlexibleFactory
{
    private readonly FlexibleFactoryPool _pool;

    public FlexibleFactory(FlexibleFactoryPool pool)
    {
        _pool = pool;
    }

    public async Task<IFabricElement> Create(AssetReference prefab, Vector3 position, Transform parent = null)
    {
        return await _pool.GetOrCreateAsync(prefab, position, parent);
    }

    public void Despawn(AssetReference prefab, IFabricElement element)
    {
        _pool.Return(prefab, element);
    }
}