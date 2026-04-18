using System;
using System.Collections.Generic;

[Serializable]
public class MedicalTestDTO
{
    public string id;
    public string name;
    public float baseDuration;
    public List<string> detectableSymptomIds;
}