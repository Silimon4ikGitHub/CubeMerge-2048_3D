using UnityEngine;
using Zenject;

public class PlaySceneInstaller : MonoInstaller
{
    [SerializeField] private GamePlayController _gamePlayController;
    [SerializeField] private CubeSpaner _cubeSpaner;
    [SerializeField] private BaseLevelData _levelData;

    public override void InstallBindings()
    {
        var playSceneServices = new PlaySceneServiceProvider();
        Container.Bind<BaseSceneServiceProvider>().FromInstance(playSceneServices).AsSingle().NonLazy();
        Container.Bind<GamePlayController>().FromInstance(_gamePlayController).AsSingle().NonLazy();
        Container.Bind<CubeSpaner>().FromInstance(_cubeSpaner).AsSingle().NonLazy();
        Container.Bind<BaseLevelData>().FromInstance(_levelData).AsSingle().NonLazy();

        Container.Bind<FlexibleFactoryPool>().FromNew().AsSingle().NonLazy();
        Container.Bind<FlexibleFactory>().FromNew().AsSingle().NonLazy();
    }
}
