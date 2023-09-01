using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersistentDataHandler : Singleton<PersistentDataHandler>
{
    private List<PersistentDataBase> _persistentDataObjects = new List<PersistentDataBase>();

    public T GetDataObject<T>(string relativeFilePath) where T: PersistentDataBase, new()
    {
        PersistentDataBase data = _persistentDataObjects.FirstOrDefault(data => data.GetType() == typeof(T));
        if (data != null)
        {
            return data as T;
        }
        else
        {
            var loadedData = JsonDataService<T>.LoadData(relativeFilePath);
            _persistentDataObjects.Add(loadedData);
            return loadedData;
        }
    }
    public void SaveDataObject<T>(T dataObject, string relativeFilePath) where T: PersistentDataBase, new()
    {
        JsonDataService<T>.SaveData(relativeFilePath, dataObject);
        if (!_persistentDataObjects.Contains(dataObject))
        {
            _persistentDataObjects.Add(dataObject);
        }
    }
}
