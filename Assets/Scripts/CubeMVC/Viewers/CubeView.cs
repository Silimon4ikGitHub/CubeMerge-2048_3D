using UnityEngine;

public class CubeView : BaseGamePlayElementView
{
    public override void Push(Vector3 direction, float force)
    {
        _rb.isKinematic = false;
        _rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }

    public override void OnSpawn(Vector3 position, Transform parent)
    {
        base.OnSpawn(position, parent);

        _rb.isKinematic = true;
    }
}
