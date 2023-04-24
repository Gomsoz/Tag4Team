using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using UnityEngine;

public static class JsonManager
{
    public static T FromJson<T>(string fileName)
    {
        string dataPath = string.Empty;
        string jsonData = string.Empty;

        dataPath = Path.Combine(Application.persistentDataPath, "Datas", $"{fileName}.json");

        try
        {
            using (FileStream fileStream = new FileStream(dataPath, FileMode.Open))
            {
                byte[] datas = new byte[fileStream.Length];
                fileStream.Read(datas, 0, datas.Length);
                fileStream.Close();
                jsonData = Encoding.UTF8.GetString(datas);
            }
        }
        catch
        {
            Debug.Log($"Load Exception : {dataPath}");
        }

        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    public static void ToJson(object obj, string fileName)
    {
        string dataPath = string.Empty;

        dataPath = Path.Combine(Application.persistentDataPath, "Datas", $"{fileName}.json");

        string json = JsonConvert.SerializeObject(obj);

        DirectoryInfo directoryInfo = new DirectoryInfo(dataPath);
        if (directoryInfo.Exists == false)
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Datas"));
        }

        try
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Create);
            byte[] datas = Encoding.UTF8.GetBytes(json);
            fileStream.Write(datas, 0, datas.Length);
            fileStream.Close();
        }
        catch
        {
            Debug.Log($"Save Exception : {dataPath}");
        }
    }

    //public static T FromJson<T>(string fileName)
    //{
    //    string dataPath = string.Empty;
    //    string jsonData = string.Empty;

    //    dataPath = Path.Combine(Application.persistentDataPath, "Datas", $"{fileName}.json");

    //    try
    //    {
    //        using (FileStream fileStream = new FileStream(dataPath, FileMode.Open))
    //        {
    //            byte[] datas = new byte[fileStream.Length];
    //            fileStream.Read(datas, 0, datas.Length);
    //            fileStream.Close();
    //            jsonData = Encoding.UTF8.GetString(datas);
    //        }
    //    }
    //    catch
    //    {
    //        Debug.Log($"Load Exception : {dataPath}");
    //    }

    //    return JsonUtility.FromJson<T>(jsonData);
    //}

    //public static void ToJson(object obj, string fileName)
    //{
    //    string dataPath = string.Empty;

    //    dataPath = Path.Combine(Application.persistentDataPath, "Datas", $"{fileName}.json");

    //    string json = JsonUtility.ToJson(obj);

    //    DirectoryInfo directoryInfo = new DirectoryInfo(dataPath);
    //    if (directoryInfo.Exists == false)
    //    {
    //        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Datas"));
    //    }

    //    try
    //    {
    //        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
    //        byte[] datas = Encoding.UTF8.GetBytes(json);
    //        fileStream.Write(datas, 0, datas.Length);
    //        fileStream.Close();
    //    }
    //    catch
    //    {
    //        Debug.Log($"Save Exception : {dataPath}");
    //    }
    //}
}
