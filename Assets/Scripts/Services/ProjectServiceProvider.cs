
public class ProjectServiceProvider
{
    public UnitySceneLoader SceneLoader { get; private set; }

    public void Initialize
    (
        UnitySceneLoader unitySceneLoader
    )
    {
        SceneLoader = unitySceneLoader;
    }
}
