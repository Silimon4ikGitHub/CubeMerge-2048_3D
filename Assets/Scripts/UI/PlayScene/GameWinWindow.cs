using UnityEngine;

public class GameWinWindow : GameResultWindow
{
    private UnitySceneLoader _unitySceneLoader => _projectServiceProvider.SceneLoader;

    public override void Initialize()
    {
        base.Initialize();

        _continueButton.onClick.AddListener(Continue);
    }

    private void Continue()
    {
        _unitySceneLoader.LoadGamelayScene();
    }
}
