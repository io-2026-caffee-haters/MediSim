public interface ISaveSystem
{
    void SaveCurrentGame(SaveData data);
    SaveData LoadSavedGame();
    bool HasSaveFile();
}