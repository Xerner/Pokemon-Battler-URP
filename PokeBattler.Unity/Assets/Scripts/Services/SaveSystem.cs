using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using PokeBattler.Common.Models;

namespace PokeBattler.Client.Services
{
    public interface ISaveSystem
    {
        public string PersistentDataPath { get; }
        Account SaveAccount(Account account);
        Account SaveAccount(Account account, string persistentDataPath);
        Account LoadAccount(string username);
        Account LoadAccount(string username, string persistentDataPath);
    }

    public class SaveSystem : ISaveSystem
    {
        public string PersistentDataPath { get => Application.persistentDataPath + "/"; }

        public Account SaveAccount(Account account) => SaveAccount(account, PersistentDataPath);

        public Account SaveAccount(Account account, string persistentDataPath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using var stream = new FileStream(PersistentDataPath + account.Username.Trim().ToLower(), FileMode.Create);
            formatter.Serialize(stream, account);
            Debug.Log($"Wrote Settings to file\n{PersistentDataPath}{account.Username.Trim().ToLower()}");
            return account;
        }

        public Account LoadAccount(string username) => LoadAccount(username, PersistentDataPath);

        public Account LoadAccount(string username, string persistentDataPath)
        {
            if (!File.Exists(persistentDataPath + username))
            {
                Debug.LogWarning($"Account with name '{username}' save file does not exist!\n\n{persistentDataPath}{username}\n");
                return null;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            var filePath = persistentDataPath + username.Trim().ToLower();
            using var stream = new FileStream(filePath, FileMode.Open);
            var settings = formatter.Deserialize(stream) as Account;
            return settings;
        }
    }
}
