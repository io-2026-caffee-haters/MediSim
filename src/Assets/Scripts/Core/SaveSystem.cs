using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    // Pomocnicza metoda, żeby nie kopiować ścieżki
    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "savegame.json");
    }

    public void SaveCurrentGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(), json);
        Debug.Log("Zapisano grę w: " + GetPath());
    }

    public SaveData LoadGame()
    {
        if (File.Exists(GetPath()))
        {
            string json = File.ReadAllText(GetPath());
            Debug.Log("Wczytano grę z: " + GetPath());
            return JsonUtility.FromJson<SaveData>(json);
        }
        
        Debug.LogWarning("Brak pliku zapisu!");
        return null;
    }
}