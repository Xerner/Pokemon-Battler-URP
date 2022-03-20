using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    private static string accountSettingsPath = Application.persistentDataPath + "/";

    public static AccountSettings SaveAccount(Account account)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(accountSettingsPath + account.settings.Username.Trim().ToLower(), FileMode.Create);
        formatter.Serialize(stream, account.settings);
        Debug.Log($"Wrote settings to file\n{accountSettingsPath}{account.settings.Username.Trim().ToLower()}");
        stream.Close();
        return account.settings;
    }

    public static AccountSettings LoadAccount(string username)
    {
        var filePath = accountSettingsPath + username.Trim().ToLower();
        if (File.Exists(accountSettingsPath + username))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            var settings = formatter.Deserialize(stream) as AccountSettings;
            stream.Close();
            return settings;
        }
        else
        {
            Debug.LogWarning($"Account with name '{username}' save file does not exist!\n\n{accountSettingsPath}{username}\n");
            return null;
        }
    }
}
