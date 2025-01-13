using Zenject;

namespace Core.Persistence
{
    public class SaveLoadSystemDependencies : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISerializer>().To<JsonSerializer>().FromNew().AsSingle();
            Container.Bind<IDataService>().To<FileService>().AsSingle();
        }
    }
}