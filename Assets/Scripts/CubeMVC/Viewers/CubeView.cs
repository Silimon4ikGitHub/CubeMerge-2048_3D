using UnityEngine;

public class CubeView : BaseGamePlayElementView, ICubeCollider
{
    public int Value { get; set; }

    [SerializeField] private CubeColisionBehaviour _colisionBehaviour;

    private PlaySceneServiceProvider _playSceneServiceProvider;
    private GamePlayController _gamePlayController => _playSceneServiceProvider.GameplayController;
    private FinishGameController _finishGameController => _playSceneServiceProvider.FinishGameController;
    private TemporaryInfo _temporaryInfo => _projectServiceProvider.TemporaryInfo;

    public override void Initialize(ProjectServiceProvider projectServiceProvider, BaseSceneServiceProvider sceneServices)
    {
        base.Initialize(projectServiceProvider, sceneServices);

        if (sceneServices is PlaySceneServiceProvider playSceneServiceProvider)
        {
            _playSceneServiceProvider = playSceneServiceProvider;
        }
        else
        {
            Debug.LogError("CubeView not initializet in currnt Scene DI");
        }
    }

    public override void Setup(BaseElementModel model)
    {
        base.Setup(model);

        if (model is CubeModel cubeModel)
        {
            Value = cubeModel.Po2;
        }
        if (_temporaryInfo.CurrentLevelData is LevelData levelData)
        {
            _colisionBehaviour.SetMinCollisionImpulse(levelData.MinCollisionImpilse);
        }

        _colisionBehaviour.Setup(this);
    }

    public override void Push(Vector3 direction, float force)
    {
        _rb.isKinematic = false;
        _rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        _currentState = ElementState.Pushed;
    }

    public override void OnSpawn(Vector3 position, Transform parent)
    {
        base.OnSpawn(position, parent);

        _rb.isKinematic = true;
        _currentState = ElementState.OnDrag;
    }

    public void OnUpgradeSpawn()
    {
        _rb.isKinematic = false;
    }

    public void OnCollisionSuccess(ICubeCollider otherCollider)
    {
        _finishGameController.CalculateScore(this);

        if (otherCollider is IFabricElement factoryElement)
        {
            factoryElement.OnDespawn();
        }

        Upgrade();
    }

    private void Upgrade()
    {
        _gamePlayController.UpgradeElement(this);
    }
}
