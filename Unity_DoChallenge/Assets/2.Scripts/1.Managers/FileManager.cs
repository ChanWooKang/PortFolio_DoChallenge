using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager
{
    const string JsonSuffix = ".json";
    string JsonPath;

    public void Init()
    {
#if UNITY_EDITOR
        JsonPath = Application.dataPath + "/Json";
#else
        JsonPath = Application.persistentDataPath + "/Json";
#endif
    }

    public void SaveJson<T>(T data, string name)
    {
        try
        {
            if (Directory.Exists(JsonPath) == false)
                Directory.CreateDirectory(JsonPath);

            if (name.Contains(JsonSuffix) == false)
                name += JsonSuffix;

            string saveData = JsonUtility.ToJson(data);
            string path = Path.Combine(JsonPath, name);
            if (File.Exists(path) == false)
                File.Create(path);

            File.WriteAllText(path, saveData);
        }
        catch (Exception ex)
        {
            Debug.Log($"FileManager : Failed To Save Json ({name})");
            Debug.Log(ex.Message);
        }
    }

    public string LoadJson(string name)
    {
        try
        {
            if (Directory.Exists(JsonPath) == false)
            {
                Directory.CreateDirectory(JsonPath);
                return null;
            }

            if (name.Contains(JsonSuffix) == false)
                name += JsonSuffix;

            string path = Path.Combine(JsonPath, name);
            if (File.Exists(path) == false)
            {
                File.Create(path);
                return null;
            }

            string saveData = File.ReadAllText(path);
            return saveData;
        }
        catch (Exception ex)
        {
            Debug.Log($"FileManager : Failed To Load Json ({name})");
            Debug.Log(ex.Message);
            return null;
        }
        
    }
}
