
public class ProjectServiceProvider
{
    public UnitySceneLoader SceneLoader { get; private set; }
    public TemporaryInfo TemporaryInfo { get; private set; }

    public void Initialize
    (
        UnitySceneLoader unitySceneLoader,
        TemporaryInfo temporaryInfo
    )
    {
        SceneLoader = unitySceneLoader;
        TemporaryInfo = temporaryInfo;
    }
}
