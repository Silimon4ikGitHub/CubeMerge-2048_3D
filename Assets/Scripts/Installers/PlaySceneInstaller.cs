using UnityEngine;
using Zenject;

public class PlaySceneInstaller : MonoInstaller
{


    public override void InstallBindings()
    {
        var playSceneServices = new PlaySceneServiceProvider();
        Container.Bind<BaseSceneServiceProvider>().FromInstance(playSceneServices).AsSingle().NonLazy();
        

    }
}
