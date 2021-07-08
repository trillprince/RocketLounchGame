using System;
using System.Threading.Tasks;
using Common.Scripts.Data;
using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference _database;

    private void Awake()
    {
        _database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        CreateNewPlayerData();
    }

    public void CreateNewPlayerData()
    {
        PlayerData defaultPlayerData = new PlayerData("Nik", 500, 500, 5);
        SaveData(PlayerData.GetDataKey(), defaultPlayerData);
    }
    
    void SaveData (string key, PlayerData data)
    {
        var jsonData = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(key,jsonData);
        Debug.Log(jsonData);
        _database.Child(key).SetRawJsonValueAsync(jsonData).ContinueWith(task =>
        {
            Debug.Log(task.Status);
        } );
    }
    

    public async Task<bool> SaveExists(string key)
    {
        var data = await _database.Child(key).GetValueAsync();
        return data.Exists;
    }

    public async Task<PlayerData> LoadData(string key)
    {
        var data = await _database.Child(key).GetValueAsync();
        if (!data.Exists)
        {
            return null;
        }
        return JsonUtility.FromJson<PlayerData>(data.GetRawJsonValue());
    }

    
    
}

