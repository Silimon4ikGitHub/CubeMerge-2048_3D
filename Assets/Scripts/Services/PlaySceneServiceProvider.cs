using UnityEngine;

public class PlaySceneServiceProvider : BaseSceneServiceProvider
{
    public GamePlayController GameplayController {  get; private set; }
    public CubeSpaner CubeSpaner { get; private set; }

    public void Initialize(GamePlayController gameplayController, CubeSpaner cubeSpaner)
    {
        GameplayController = gameplayController;
        CubeSpaner = cubeSpaner;
    }
}
