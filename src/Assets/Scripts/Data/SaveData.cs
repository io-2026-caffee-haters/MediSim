using System;
using System.Collections.Generic;

[Serializable] 
public class SaveData
{
    public int currentScore;
    public float remainingTime;
    public string currentDiseaseId;
    public string playerNotes;

    public List<int> cooldownTestIds = new List<int>();
    public List<float> cooldownTimes = new List<float>();
}