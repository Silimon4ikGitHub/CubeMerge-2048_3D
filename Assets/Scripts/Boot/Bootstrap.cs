using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private BaseLevelData _firstLevel;

    [Inject] private ProjectServiceProvider _projectServiceProvider;
    [Inject] private UnitySceneLoader _sceneLoader;
    [Inject] private TemporaryInfo _temporaryInfo;

    private void Start()
    {
        InitializeDependecies();
        SelectLevel();
        LoadGameplayScene();
    }

    private void InitializeDependecies()
    {
        _projectServiceProvider.Initialize(_sceneLoader, _temporaryInfo);
    }

    private void LoadGameplayScene()
    {
        _sceneLoader.LoadGamelayScene();
    }

    private void SelectLevel()
    {
        if (_temporaryInfo.CurrentLevelData == null)
        {
            _temporaryInfo.ChangeLevel(_firstLevel);
        }
    }
}
