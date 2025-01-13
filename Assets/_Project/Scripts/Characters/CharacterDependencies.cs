using Zenject;

namespace Core.Characters
{
    public class CharacterDependencies : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICharacterMover>().To<HorizontalDrifting>().FromNew().AsTransient();
        }
    }
}