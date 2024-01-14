using PokeBattler.Unity;
using Zenject;
using PokeBattler.Client.Services;

public class GlobalInstaller : MonoInstaller
{
    public AccountSO DefaultAccount;

    public override void InstallBindings()
    {
        #region Singletons
        // Services
        Container.BindInstance(HubConnectionService.Connection).AsSingle().NonLazy();
        //Container.Bind<IServerService>().To<ServerService>().AsSingle().NonLazy();
        Container.Bind<ITrainersService>().To<TrainersService>().AsSingle().NonLazy();
        Container.Bind<IGameService>().To<GameService>().AsSingle().NonLazy();
        Container.Bind<IClientService>().To<ClientService>().AsSingle().NonLazy();
        Container.Bind<IPokemonPoolService>().To<PokemonPoolService>().AsSingle().NonLazy();
        Container.Bind<IShopService>().To<ShopService>().AsSingle().NonLazy();
        Container.Bind<IGameObjectService>().To<GameObjectService>().AsSingle().NonLazy();

        // UI/View Services
        Container.Bind<IMainMenuService>().To<MainMenuService>().AsSingle().NonLazy();
        Container.Bind<UIPersistentStatus>().FromComponentInHierarchy().AsSingle();

        // Scriptable Objects
        Container.BindInstance(DefaultAccount.Account).AsSingle().NonLazy();
        #endregion
    }
}
