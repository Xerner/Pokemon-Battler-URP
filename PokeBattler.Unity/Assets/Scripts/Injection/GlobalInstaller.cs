using Microsoft.Extensions.Hosting;
using PokeBattler.Core;
using PokeBattler.Unity;
using PokeBattler.Client.Controllers.HubConnections;
using PokeBattler.Services;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public AccountSO DefaultAccount;

    public override void InstallBindings()
    {
        Container.Bind<ITrainerHubConnection>().To<TrainerHubConnection>().AsSingle();
        Container.Bind<ITrainersService>().To<TrainersService>().AsSingle();
        Container.Bind<UIPersistentStatus>().FromComponentInHierarchy().AsSingle();
        Container.BindInstance(DefaultAccount);
        //Container.Bind<ILogger>();
    }
}
