using UnityEngine;

public class PlaySceneServiceProvider : BaseSceneServiceProvider
{
    public GamePlayController GameplayController {  get; private set; }
    public CubeSpaner CubeSpaner { get; private set; }
    public FlexibleFactory Factory { get; set; }
    public FlexibleFactoryPool FlexibleFactoryPool { get; set; }
    public InputController InputController { get; private set; }
    public FinishGameController FinishGameController { get; private set; }
    public PlaySceneUI PlaySceneUI { get; private set; }

    public void Initialize(GamePlayController gameplayController, CubeSpaner cubeSpaner, FlexibleFactory flexibleFactory, FlexibleFactoryPool flexibleFactoryPool, InputController inputController, FinishGameController finishGameController, PlaySceneUI playSceneUI)
    {
        GameplayController = gameplayController;
        CubeSpaner = cubeSpaner;
        Factory = flexibleFactory;
        FlexibleFactoryPool = flexibleFactoryPool;
        InputController = inputController;
        FinishGameController = finishGameController;
        PlaySceneUI = playSceneUI;
    }
}
