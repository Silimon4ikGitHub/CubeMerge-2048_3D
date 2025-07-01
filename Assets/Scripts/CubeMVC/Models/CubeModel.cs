using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "Elements/New Cube Data")]
public class CubeModel : BaseElementModel
{
    [SerializeField] protected int _value;
    [SerializeField] protected CubeModel _upgradeModel;
}
