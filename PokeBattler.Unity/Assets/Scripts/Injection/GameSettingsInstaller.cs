using PokeBattler.Models;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameSettings DefaultGameSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(DefaultGameSettings);
    }
}