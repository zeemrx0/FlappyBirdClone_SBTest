using Zenject;

namespace LNE.Core
{
  public class GameCoreInstaller : MonoInstaller<GameCoreInstaller>
  {
    public override void InstallBindings()
    {
      Container.Bind<GameOverManager>().FromComponentInHierarchy().AsSingle();
    }
  }
}
