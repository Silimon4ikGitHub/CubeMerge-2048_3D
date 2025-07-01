using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class BaseElementModel : ScriptableObject
{
    public AssetReference Prefab => _prefab;

    [SerializeField] protected AssetReference _prefab;
}
