using UnityEngine;
using Zenject;

public class PlaySceneInstaller : MonoInstaller
{
    [SerializeField] private GamePlayController _gamePlayController;
    [SerializeField] private CubeSpaner _cubeSpaner;
    [SerializeField] private BaseLevelData _levelData;
    [SerializeField] private InputController _inputController;
    [SerializeField] private FinishGameController _finishGameController;
    [SerializeField] private PlaySceneUI _playSceneUI;

    public override void InstallBindings()
    {
        var playSceneServices = new PlaySceneServiceProvider();
        Container.Bind<BaseSceneServiceProvider>().FromInstance(playSceneServices).AsSingle().NonLazy();
        Container.Bind<GamePlayController>().FromInstance(_gamePlayController).AsSingle().NonLazy();
        Container.Bind<CubeSpaner>().FromInstance(_cubeSpaner).AsSingle().NonLazy();
        Container.Bind<BaseLevelData>().FromInstance(_levelData).AsSingle().NonLazy();
        Container.Bind<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
        Container.Bind<FinishGameController>().FromInstance(_finishGameController).AsSingle().NonLazy();
        Container.Bind<PlaySceneUI>().FromInstance(_playSceneUI).AsSingle().NonLazy();

        Container.Bind<FlexibleFactoryPool>().FromNew().AsSingle().NonLazy();
        Container.Bind<FlexibleFactory>().FromNew().AsSingle().NonLazy();
    }
}
