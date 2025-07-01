using UnityEngine;

public abstract class BaseGamePlayElementView : MonoBehaviour, IFabricElement
{
    public int Id { get; set; }
    public Rigidbody Rigidbody => _rb;

    [SerializeField] protected Rigidbody _rb;

    protected BaseElementModel _myModel;
    protected BaseSceneServiceProvider _sceneServiceProvider;

    public abstract void Push(Vector3 direction, float force);

    public virtual void Initialize(BaseSceneServiceProvider sceneServices)
    {
        _sceneServiceProvider = sceneServices;
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

    public virtual void Setup(BaseElementModel model)
    {
        _myModel = model;

        if (_myModel == null)
        {
            Debug.LogError($"Created view without model {gameObject.name}");
        }
    }

    public BaseElementModel GetModel()
    {
        return _myModel;
    }
}
