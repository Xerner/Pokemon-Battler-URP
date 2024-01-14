using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PokeBattler.Common.Models;

namespace PokeBattler.Common.Services {
    public static class SaveSystem
    {
        private static string accountSettingsPath = Application.persistentDataPath + "/";

        public static Account SaveAccount(Account account)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(accountSettingsPath + account.Username.Trim().ToLower(), FileMode.Create);
            formatter.Serialize(stream, account);
            Debug.Log($"Wrote Settings to file\n{accountSettingsPath}{account.Username.Trim().ToLower()}");
            stream.Close();
            return account;
        }

        public static Account LoadAccount(string username)
        {
            if (!File.Exists(accountSettingsPath + username))
            {
                Debug.LogWarning($"Account with name '{username}' save file does not exist!\n\n{accountSettingsPath}{username}\n");
                return null;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            var filePath = accountSettingsPath + username.Trim().ToLower();
            using var stream = new FileStream(filePath, FileMode.Open);
            var settings = formatter.Deserialize(stream) as Account;
            return settings;
        }
    }
}