using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class GameResultWindow : MonoBehaviour
{
    [SerializeField] protected Button _continueButton;

    [Inject] protected ProjectServiceProvider _projectServiceProvider;
    [Inject] protected BaseSceneServiceProvider _sceneServiceProvider;
    protected PlaySceneServiceProvider _playSceneServiceProvider;
    public virtual void Initialize()
    {
        if (_sceneServiceProvider is PlaySceneServiceProvider playSceneServiceProvider)
        {
            _playSceneServiceProvider = playSceneServiceProvider;
        }
        else
        {
            Debug.LogError("GameResultWindow not initialized, check DI");
        }

        gameObject.SetActive(false);
    }
}
