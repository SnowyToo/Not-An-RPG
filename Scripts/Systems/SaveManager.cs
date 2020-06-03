using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public static void SaveStats(PlayerStats stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, stats);
        stream.Close();
    }

    public static void SaveStats()
    {
        PlayerStats stats = new PlayerStats();
        SaveStats(stats);
    }

    public static PlayerStats LoadStats()
    {
        string path = Application.persistentDataPath + "/player.bin";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerStats stats = formatter.Deserialize(stream) as PlayerStats;
            stream.Close();
            return stats;
        }
        else
        {
            Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }
}
