using UnityEngine;

public abstract class BaseElementModel : ScriptableObject
{
    public BaseGamePlayElementView Prefab => _prefab;

    [SerializeField] protected BaseGamePlayElementView _prefab;
}
