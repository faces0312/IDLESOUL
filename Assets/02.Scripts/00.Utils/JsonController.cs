using UnityEngine;
using System.IO;
using System.Text;

public class JsonController
{
    // Application.persistentDataPath : C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]

    public UserDB LoadUserData(string path)
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            string json = File.ReadAllText(Application.persistentDataPath + path);
            Debug.Log("UserData loded to: " + path);
            return JsonUtility.FromJson<UserDB>(json);
        }

        Debug.LogWarning("UserData file not found!");
        return null;
    }

    public void SaveUserData(UserDB saveData, string path)
    {
        string jsonUserData = JsonUtility.ToJson(saveData, true);
        FileStream fileStream = new FileStream(Application.persistentDataPath + path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonUserData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();

        //File.WriteAllText(path, jsonUserData);
        Debug.Log("UserData saved to: " + Application.persistentDataPath + path); 
    }
}
