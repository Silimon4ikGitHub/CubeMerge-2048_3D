using UnityEngine;

public interface IFabricElement
{
    public int Id {  get; set; }
    void Initialize(BaseSceneServiceProvider sceneServices);
    void OnSpawn(Vector3 position, Transform parent);
    void OnDespawn();
}
