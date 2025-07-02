using UnityEngine;

public class CubeColisionBehaviour : MonoBehaviour
{
    private ICubeCollider _myCollider;

    private float _minImpulseThreshold = 0;

    public void Setup(ICubeCollider collider)
    {
        _myCollider = collider;

    }

    public void SetMinCollisionImpulse(float collisionMinImpulse)
    {
        _minImpulseThreshold = collisionMinImpulse;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherElement = collision.collider.GetComponent<ICubeCollider>();

        if (otherElement != null)
        {
            float impulseMagnitude = collision.impulse.magnitude;

            Debug.Log($"Impulse magnitude: {impulseMagnitude}");
            Debug.Log($"minImpulseThreshold: {_minImpulseThreshold}");

            if (impulseMagnitude < _minImpulseThreshold)
            {
                Debug.Log("Impulse too low. Collision ignored.");
                return;
            }

            if (_myCollider.Id > otherElement.Id && otherElement.Value == _myCollider.Value)
            {
                _myCollider.OnCollisionSuccess(otherElement);
            }
        }
    }
}
