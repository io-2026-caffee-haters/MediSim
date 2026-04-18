using System;

public class JsonLocalSaveSystem : ISaveSystem
{
    private readonly string _saveFileName;

    public JsonLocalSaveSystem(string saveFileName = "savegame.json")
    {
        // _saveFileName = saveFileName;
    }

    public void SaveCurrentGame(SaveData data)
    {
        throw new NotImplementedException();
    }

    public SaveData LoadSavedGame()
    {
        throw new NotImplementedException();
    }

    public bool HasSaveFile()
    {
        throw new NotImplementedException();
    }
}