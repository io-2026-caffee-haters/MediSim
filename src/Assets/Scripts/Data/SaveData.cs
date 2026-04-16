using System;
using System.Collections.Generic;

[Serializable] 
public class SaveData
{
    public int currentScore;
    public float remainingTime;
    public string currentDiseaseId;
    public string playerNotes;


    public List<string> discoveredSymptomIds = new List<string>();

    public List<TestCooldownSave> activeCooldowns = new List<TestCooldownSave>();
}

[Serializable]
public class TestCooldownSave
{
    public string testId;
    public float remainingTime;
}