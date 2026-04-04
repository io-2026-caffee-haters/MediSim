using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int savedScore;
    public float savedTime;
    public string currentPatientDiseaseId;
    public string playerNotes;
    public Dictionary<int, float> testCooldowns = new Dictionary<int, float>();
}