using System.Collections.Generic;


[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public class LevelSaveEntry
    {
        public string sceneName;
        public ushort bestTime;
        public byte stars;
    }

    public List<LevelSaveEntry> levels = new();

    public LevelSaveEntry GetOrCreate(string sceneName)
    {
        foreach (var entry in levels)
            if (entry.sceneName == sceneName)
                return entry;

        var newEntry = new LevelSaveEntry { sceneName = sceneName };
        levels.Add(newEntry);
        return newEntry;
    }
}
