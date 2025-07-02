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

    public async Task<IFactoryElement> Create(IFactoryElementModel model, Vector3 position, Transform parent = null)
    {
        return await _pool.GetOrCreateAsync(model, position, parent);
    }

    public void Despawn(IFactoryElementModel model, IFactoryElement element)
    {
        _pool.Return(model, element);
    }
}