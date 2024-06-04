using Zenject;

namespace LNE.Core
{
  public class GameCoreInstaller : MonoInstaller<GameCoreInstaller>
  {
    public override void InstallBindings()
    {
      Container.Bind<GamePlayManager>().FromComponentInHierarchy().AsSingle();
      Container.Bind<SavingManager>().FromComponentInHierarchy().AsSingle();
      Container.Bind<NetworkSavingManager>().FromComponentInHierarchy().AsSingle();
    }
  }
}
