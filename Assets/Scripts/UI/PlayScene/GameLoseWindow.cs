using UnityEngine;

public class GameLoseWindow : GameResultWindow
{
    private UnitySceneLoader _unitySceneLoader => _projectServiceProvider.SceneLoader;

    public override void Initialize()
    {
        base.Initialize();

        _continueButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        _unitySceneLoader.LoadGamelayScene();
    }
}
