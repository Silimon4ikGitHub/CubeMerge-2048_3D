using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "Elements/New Cube Data")]
public class CubeModel : BaseElementModel
{
    public int Po2 => _po2;
    public CubeModel UpgradeModel => _upgradeModel;

    [SerializeField] protected int _po2;
    [SerializeField] protected CubeModel _upgradeModel;
}
