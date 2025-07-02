using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class BaseElementModel : ScriptableObject, IFactoryElementModel
{
    public AssetReference Prefab => _prefab;
    public FactoryElementType Type => _type;

    [SerializeField] protected AssetReference _prefab;
    [SerializeField] protected FactoryElementType _type;
}
