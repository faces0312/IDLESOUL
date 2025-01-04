using UnityEngine;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using DG.Tweening.Plugins.Core.PathCore;

public class JsonController
{
    // Application.persistentDataPath : C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]
    [DllImport("__Internal")]
    private static extern void SyncFilesToIndexedDB();
    [DllImport("__Internal")]
    private static extern void LoadFilesFromIndexedDB();
    public UserDB LoadUserData(string path)
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            string json = File.ReadAllText(Application.persistentDataPath + path);
            Debug.Log("UserData loded to: " + path);
            LoadFilesFromIndexedDB(); // 초기화 시 데이터 로드
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
        SyncFilesToIndexedDB(); // 저장 후 동기화
        fileStream.Close();

        //File.WriteAllText(path, jsonUserData);
        Debug.Log("UserData saved to: " + Application.persistentDataPath + path); 
    }

    public bool CheckJsonData(string path)
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            Debug.Log($"JSON file exists at: {Application.persistentDataPath + path}");
            return true;
        }
        else
        {
            Debug.LogWarning($"JSON file not found at: {Application.persistentDataPath + path}");
            return false;
        }
    }

    public void DeleteJsonData(string path) 
    {
        Debug.Log($"JSON file found at: {Application.persistentDataPath + path}. Deleting it...");
        File.Delete(Application.persistentDataPath + path); // 파일 삭제
        SyncFilesToIndexedDB(); // 저장 후 동기화
        Debug.Log($"JSON file deleted: {Application.persistentDataPath + path}");
    }
}
