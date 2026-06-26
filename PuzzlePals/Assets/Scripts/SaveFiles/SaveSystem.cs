using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string path = Application.persistentDataPath + "/levelsData.json";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static SaveData Load()
    {
        if (!File.Exists(path))
            return new SaveData();

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }
}
