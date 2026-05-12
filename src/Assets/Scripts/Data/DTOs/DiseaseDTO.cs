using System;
using System.Collections.Generic;

[Serializable]
public class DiseaseDTO
{
    public int id;
    public string name;
    public List<int> symptomIds;
}