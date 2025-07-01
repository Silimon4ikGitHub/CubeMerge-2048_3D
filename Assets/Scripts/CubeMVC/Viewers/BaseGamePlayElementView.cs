using UnityEngine;

public class BaseGamePlayElementView : MonoBehaviour, IFabricElement
{
    public int Id { get; set; }

    protected BaseElementModel _myModel;
    protected BaseSceneServiceProvider _sceneServiceProvider;

    public void Initialize(BaseSceneServiceProvider sceneServices)
    {
        _sceneServiceProvider = sceneServices;
    }

    public void Setup(BaseElementModel model)
    {
        _myModel = model;

        if (_myModel == null)
        {
            Debug.LogWarning($"Created view without model {gameObject.name}");
        }
    }

    public virtual void OnSpawn(Vector3 position, Transform parent)
    {
        gameObject.transform.position = position;
        gameObject.transform.SetParent(parent, false);

        gameObject.SetActive(true);
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}
