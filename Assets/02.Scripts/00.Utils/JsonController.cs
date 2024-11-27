using UnityEngine;
using System.IO;

public class JsonController
{
    public UserData LoadUserData(string path)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log("UserData loded to: " + path);
            return JsonUtility.FromJson<UserData>(json);
        }
        Debug.LogWarning("UserData file not found!");
        return null;
    }

    public void SaveUserData(UserData userData, string path)
    {
        string jsonUserData = JsonUtility.ToJson(userData, true);
        File.WriteAllText(path, jsonUserData);
        Debug.Log("UserData saved to: " + path);
    }
}
