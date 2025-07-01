using UnityEngine;

public class CubeColisionBehaviour : MonoBehaviour
{
    private ICubeCollider _myCollider;

    public void Setup(ICubeCollider collider)
    {
        _myCollider = collider;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherElement = collision.collider.GetComponent<ICubeCollider>();

        if (otherElement != null)
        {
            if (_myCollider.Id > otherElement.Id && otherElement.Value == _myCollider.Value)
            {
                _myCollider.OnCollisionSuccess(otherElement);
            }
        }
    }
}
