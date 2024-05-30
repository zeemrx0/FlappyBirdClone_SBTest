using Zenject;

namespace LNE.Inputs
{
  public class PlayerInputInstaller : MonoInstaller<PlayerInputInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .Bind<PlayerInputManager>()
        .FromComponentInHierarchy()
        .AsSingle();
    }
  }
}
