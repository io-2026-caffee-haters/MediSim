using System;
using System.Collections.Generic;

[Serializable]
public class MedicalTestDTO
{
    public int id;
    public string name;
    public float cooldown;
    public List<int> detectableSymptomIds;
    public float timeCost;
}