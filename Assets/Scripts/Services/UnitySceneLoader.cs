using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitySceneLoader
{
    private const string _menuScene = "Bootstrap";
    private const string _gameplayScene = "GameplayTest";

    public void StartGamelayScene()
    {
        LoadScene(_gameplayScene);
    }

    private void LoadScene(string sceneName)
    {
        Debug.Log("Load Scene with name: " + sceneName);

        OnSceneChange();
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneChange()
    {

    }
}
