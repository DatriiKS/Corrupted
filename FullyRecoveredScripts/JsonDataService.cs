using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class JsonDataService<T> where T : PersistentDataBase, new()
{
    public static bool SaveData(string relativePath, T data)
    {
        string path = Application.persistentDataPath + relativePath;
        var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        try
        {
            if (File.Exists(path))
            {
                Debug.Log("<color=green>Data exists. Rewriting file!</color>");
                File.Delete(path);
            }
            else
            {
                Debug.Log("<color=yellow>Data does not exist. Writing a new file!</color>");
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data,Formatting.Indented, jsonSettings));
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogError($"Unable to save data due to: {exception.Message} {exception.StackTrace}");
            return false;
        }
    }
    public static T LoadData(string relativePath)
    {
        string path = Application.persistentDataPath + relativePath;
        if (!File.Exists(path))
        {
            Debug.LogError($"Cant load file at {path}. File does not exist!");
            T newlyCreatedDataObj = new T();
            SaveData(relativePath, newlyCreatedDataObj);
            return newlyCreatedDataObj;
        }
        string jsonString = File.ReadAllText(path);
        T data = JsonConvert.DeserializeObject<T>(jsonString);
	Debug.Log($"Data of type {data.GetType()}<color=green>loaded successfully</color>");
        return data;
        try
        {
            string jsonString = File.ReadAllText(path);
            T data = JsonConvert.DeserializeObject<T>(jsonString);
            Debug.Log($"Data of type {data.GetType()}<color=green>loaded successfully</color>");
            return data;
        }
        catch (Exception exception)
        {
            Debug.LogError($"Failed to load data due to: {exception.Message} {exception.StackTrace}");
            throw exception;
        }
    }
}
