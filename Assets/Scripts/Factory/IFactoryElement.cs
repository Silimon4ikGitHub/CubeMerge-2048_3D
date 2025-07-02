using UnityEngine;

public interface IFactoryElement
{
    public bool Active { get; }
    public int Id {  get; set; }
    void Initialize(ProjectServiceProvider projectServiceProvider, BaseSceneServiceProvider sceneServices);
    void OnSpawn(Vector3 position, Transform parent);
    void OnDespawn();
}
