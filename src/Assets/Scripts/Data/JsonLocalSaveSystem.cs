using System;
using System.IO; // Wymagane do pracy z systemem plików (File, Path)
using UnityEngine;

public class JsonLocalSaveSystem : ISaveSystem
{
    private readonly string _saveFilePath;

    public JsonLocalSaveSystem(string saveFileName = "savegame.json")
    {
        _saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
    }

    public void SaveCurrentGame(SaveData data)
    {
        // 1. Zamieniamy obiekt C# na tekst JSON. 
        string json = JsonUtility.ToJson(data, true);

        // 2. Tworzymy plik (lub nadpisujemy istniejący) i zapisujemy do niego tekst.
        File.WriteAllText(_saveFilePath, json);
    }

    public SaveData LoadSavedGame()
    {
        // Zabezpieczenie na wypadek, gdybyśmy próbowali wczytać grę bez pliku zapisu
        if (!HasSaveFile()) return null;

        // 1. Wczytujemy całą zawartość pliku tekstowego do zmiennej
        string json = File.ReadAllText(_saveFilePath);

        // 2. Deserializujemy tekst z powrotem na obiekt typu SaveData
        return JsonUtility.FromJson<SaveData>(json);
    }

    public bool HasSaveFile()
    {
        // Metoda File.Exists natychmiast sprawdza, czy dany plik fizycznie znajduje się na dysku
        return File.Exists(_saveFilePath);
    }
}