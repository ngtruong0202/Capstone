using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Database;
using Newtonsoft.Json;

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

    public void LoadDataFn<T>(string path, System.Action<T> callback)
    {
        StartCoroutine(LoadDataEnum(path, callback));
    }

    IEnumerator LoadDataEnum<T>(string path, System.Action<T> callback)
    {
        var serverData = dbRef.Child(path).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        print("process is complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
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
}