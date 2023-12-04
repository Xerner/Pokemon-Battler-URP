using PokeBattler.Unity;
using Zenject;
using PokeBattler.Client.Services;
using PokeBattler.Client.Controllers;
using PokeBattler.Client.Models.SO;

public class GlobalInstaller : MonoInstaller
{
    public AccountSO DefaultAccount;
    public GameSettingsSO DefaultGameSettings;

    public override void InstallBindings()
    {
        #region Singletons
        // Services
        Container.BindInstance(HubConnectionService.Connection).AsSingle().NonLazy();
        //Container.Bind<IServerService>().To<ServerService>().AsSingle().NonLazy();
        Container.Bind<ITrainersService>().To<TrainersService>().AsSingle().NonLazy();
        Container.Bind<IGameService>().To<GameService>().AsSingle().NonLazy();
        Container.Bind<IClientService>().To<ClientService>().AsSingle().NonLazy();

        // Controllers
        Container.Bind<GameController>().AsSingle().NonLazy();
        Container.Bind<TrainerController>().AsSingle().NonLazy();
        Container.Bind<ShopController>().AsSingle().NonLazy();

        // UI Services
        Container.Bind<UIPersistentStatus>().FromComponentInHierarchy().AsSingle();


        // Scriptable Objects
        Container.BindInstance(DefaultAccount.Account).AsSingle().NonLazy();
        Container.BindInstance(DefaultGameSettings).AsSingle().NonLazy();
        #endregion
    }
}
