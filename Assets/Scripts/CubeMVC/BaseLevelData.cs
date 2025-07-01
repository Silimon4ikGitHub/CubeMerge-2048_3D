using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLevelData : ScriptableObject
{
    public List<SpawnElementWraper> ElementsToSpawn => _elementsToSpawn;

    [SerializeField] protected List<SpawnElementWraper> _elementsToSpawn;
}
