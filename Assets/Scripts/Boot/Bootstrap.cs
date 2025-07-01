using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [Inject] private ProjectServiceProvider _projectServiceProvider;
    [Inject] private UnitySceneLoader _sceneLoader;

    private void Start()
    {
        InitializeDependecies();

        LoadGameplayScene();
    }

    private void InitializeDependecies()
    {
        _projectServiceProvider.Initialize(_sceneLoader);
    }

    private void LoadGameplayScene()
    {
        _sceneLoader.LoadGamelayScene();
    }
}
