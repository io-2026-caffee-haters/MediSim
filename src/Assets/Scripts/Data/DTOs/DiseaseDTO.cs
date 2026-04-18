using System;
using System.Collections.Generic;

[Serializable]
public class DiseaseDTO
{
    public string id;
    public string name;
    public List<string> symptomIds;
}