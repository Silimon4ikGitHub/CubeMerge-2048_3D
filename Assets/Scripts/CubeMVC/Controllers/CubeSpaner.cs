using UnityEngine;

public class CubeSpaner : MonoBehaviour
{
    [SerializeField] private Transform _spawnArea;

    private Vector3 GetRandomPosition()
    {
        if (_spawnArea == null)
        {
            Debug.LogWarning("SpawnerSource is not assigned!");
            return Vector3.zero;
        }

        var bounds = GetWorldBounds(_spawnArea);

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        Debug.Log("RESULT = " + new Vector3(x, y, z).ToString());

        return new Vector3(x, y, z);
    }

    private Bounds GetWorldBounds(Transform t)
    {
        var scale = t.lossyScale;
        var center = t.position;
        var size = Vector3.Scale(Vector3.one, scale);
        return new Bounds(center, size);
    }
}
