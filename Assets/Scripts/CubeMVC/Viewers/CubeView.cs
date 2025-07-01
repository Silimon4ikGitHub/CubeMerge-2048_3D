using UnityEngine;

public class CubeView : BaseGamePlayElementView
{
    public override void Push(Vector3 direction, float force)
    {
        _rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }
}
