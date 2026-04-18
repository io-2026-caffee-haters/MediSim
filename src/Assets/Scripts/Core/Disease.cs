using System.Collections.Generic;

public class Disease
{
    public string Id { get; }
    public string Name { get; }
    public List<Symptom> Symptoms { get; }

    public Disease(string id, string name, List<Symptom> symptoms)
    {
        
    }
}