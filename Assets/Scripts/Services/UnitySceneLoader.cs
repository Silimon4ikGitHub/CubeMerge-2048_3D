using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class UnitySceneLoader
{
    private const string _menuScene = "Assets/Scenes/Game/Bootstrap.unity";
    private const string _gameplayScene = "Assets/Scenes/Game/GameplayScene.unity";

    public async void LoadGamelayScene()
    {
        await LoadSceneAsync(_gameplayScene);
    }

    private async Task LoadSceneAsync(string sceneName)
    {
        Debug.Log("Load Addressable Scene: " + sceneName);

        OnSceneChange();

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Failed to load scene: " + sceneName);
        }
    }

    private void OnSceneChange()
    {

    }
}
