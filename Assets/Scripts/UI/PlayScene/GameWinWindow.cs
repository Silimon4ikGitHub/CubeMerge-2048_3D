using UnityEngine;

public class GameWinWindow : GameResultWindow
{
    private UnitySceneLoader _unitySceneLoader => _projectServiceProvider.SceneLoader;
    private TemporaryInfo _temporaryInfo => _projectServiceProvider.TemporaryInfo;

    public override void Initialize()
    {
        base.Initialize();

        _continueButton.onClick.AddListener(Continue);
    }

    private void Continue()
    {
        if (_temporaryInfo.CurrentLevelData is LevelData levelData)
        {
            _temporaryInfo.ChangeLevel(levelData.NextLevel);
        }
        
        _unitySceneLoader.LoadGamelayScene();
    }
}
