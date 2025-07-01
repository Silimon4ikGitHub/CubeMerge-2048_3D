using UnityEngine;
using Zenject;

public class PlaySceneBoot : MonoBehaviour
{
    [Inject] private BaseSceneServiceProvider _baseServiceProvider;

    private PlaySceneServiceProvider _playSceneServiceProvider;

    public void Start()
    {
        InitializeDependencies();
    }

    private void InitializeDependencies()
    {
        if (_baseServiceProvider is PlaySceneServiceProvider playSceneServices)
        {
            _playSceneServiceProvider = playSceneServices;
        }
        else
        {
            Debug.Log("No Services loaded on boot");
        }


    }
}
