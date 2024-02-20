using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PokeBattler.Common
{
    public interface IAppConfig
    {
        public string ArenaSceneName { get; }
        public string MainMenuSceneName { get; }
        public int PlayerCount { get; }
    }

    [CreateAssetMenu(fileName = "New App Config", menuName = "PokeBattler/App Config")]
    public class AppConfig : ScriptableObjectInstaller<AppConfig>, IAppConfig
    {
        [SerializeField] string arenaSceneName = "ArenaScene";
        [SerializeField] string mainMenuSceneName = "MainMenuScene";
        [SerializeField] int playerCount = 8;

        public string ArenaSceneName { get => arenaSceneName; }
        public string MainMenuSceneName { get => mainMenuSceneName; }
        public int PlayerCount { get => playerCount; }

        public override void InstallBindings()
        {
            Container.BindInstance<IAppConfig>(this).AsSingle();
        }
    }
}
