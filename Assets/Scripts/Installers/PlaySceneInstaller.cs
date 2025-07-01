using UnityEngine;
using Zenject;

public class PlaySceneInstaller : MonoInstaller
{
    [SerializeField] private GamePlayController _gamePlayController;
    [SerializeField] private CubeSpaner _cubeSpaner;

    public override void InstallBindings()
    {
        var playSceneServices = new PlaySceneServiceProvider();
        Container.Bind<BaseSceneServiceProvider>().FromInstance(playSceneServices).AsSingle().NonLazy();
        Container.Bind<GamePlayController>().FromInstance(_gamePlayController).AsSingle().NonLazy();
        Container.Bind<CubeSpaner>().FromInstance(_cubeSpaner).AsSingle().NonLazy();
        

    }
}
