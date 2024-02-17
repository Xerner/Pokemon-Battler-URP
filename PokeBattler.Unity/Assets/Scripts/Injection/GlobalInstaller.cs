using Zenject;
using PokeBattler.Unity;
using PokeBattler.Client.Services;
using Serilog;
using Serilog.Sinks.Unity3D;

public class GlobalInstaller : MonoInstaller
{
    public AccountSO DefaultAccount;

    public override void InstallBindings()
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Unity3D()
            .CreateLogger();

        #region Singletons

        // Services
        Container.BindInstance<ILogger>(logger).AsSingle();

        Container.BindInstance(HubConnectionService.Connection).AsSingle().NonLazy();
        //Container.Bind<IServerService>().To<ServerService>().AsSingle().NonLazy();
        Container.Bind<ITrainersService>().To<TrainersService>().AsSingle().NonLazy();
        Container.Bind<IGameService>().To<GameService>().AsSingle().NonLazy();
        Container.Bind<IClientService>().To<ClientService>().AsSingle().NonLazy();
        Container.Bind<IPokemonPoolService>().To<PokemonPoolService>().AsSingle().NonLazy();
        Container.Bind<IShopService>().To<ShopService>().AsSingle().NonLazy();
        Container.Bind<IGameObjectService>().To<GameObjectService>().AsSingle().NonLazy();
        Container.Bind<ISaveSystem>().To<SaveSystem>().AsSingle().NonLazy();
        Container.Bind<IViewManagerService>().To<ViewManagerService>().AsSingle().NonLazy();

        // UI/View Services
        Container.Bind<UIPersistentStatus>().FromComponentInHierarchy().AsSingle();

        // Scriptable Objects
        Container.BindInstance(DefaultAccount.Account).AsSingle().NonLazy();
        #endregion
    }
}
