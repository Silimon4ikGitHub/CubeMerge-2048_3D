using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLevelData : ScriptableObject
{
    [SerializeField] protected List<BaseElementModel> _allModels;
}
