using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using StreamWriter writer = new StreamWriter(fileStream);
        writer.Write(json);
    }

    private static string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public static string ReadFromFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string fileContents = reader.ReadToEnd();
        reader.Close();
        return fileContents;
    }
}
