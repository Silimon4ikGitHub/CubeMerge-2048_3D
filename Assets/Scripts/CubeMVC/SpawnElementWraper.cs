using UnityEngine;

[System.Serializable]
public class SpawnElementWraper
{
    public BaseElementModel Model => _model;
    public int SpanChancePercent => _spanChancePercent;

    [SerializeField] private BaseElementModel _model;
    [SerializeField] private int _spanChancePercent;
}
