using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Database;
using Newtonsoft.Json;
using System.Threading.Tasks;

[Serializable]

public class DataSever : MonoBehaviour
{
    public static DataSever Instance;
    DatabaseReference dbRef;

    private void Awake()
    {
        Instance = this;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveDataFn<T>( string path,T data)
    {
        string json = JsonConvert.SerializeObject(data);
        dbRef.Child(path).SetRawJsonValueAsync(json);
    }

    public async void LoadDataFn<T>(string path, System.Action<T> callback)
    {
        await LoadDataAsync(path, callback);
    }

    public async Task LoadDataAsync<T>(string path, System.Action<T> callback)
    {
        var serverData = await dbRef.Child(path).GetValueAsync();

        print("process is complete");

        if (serverData.Exists)
        {
            string jsonData = serverData.GetRawJsonValue();

            if (!string.IsNullOrEmpty(jsonData))
            {
                print("server data found");

                T data = JsonConvert.DeserializeObject<T>(jsonData);
                callback(data);
            }
            else
            {
                print("no data found");
                callback(default(T));
            }
        }
        else
        {
            print("no data found");
            callback(default(T));
        }
    }

}