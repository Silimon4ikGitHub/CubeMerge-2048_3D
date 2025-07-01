using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ProjectServiceProvider>().FromNew().AsSingle().NonLazy();
        Container.Bind<UnitySceneLoader>().FromNew().AsSingle().NonLazy();
        Container.Bind<TemporaryInfo>().FromNew().AsSingle().NonLazy();
    }
}
