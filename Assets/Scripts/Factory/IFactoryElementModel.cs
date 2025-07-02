using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IFactoryElementModel
{
    public AssetReference Prefab { get; }
    public FactoryElementType Type { get; }
}
