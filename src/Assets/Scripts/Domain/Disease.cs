using System.Collections.Generic;

public class Disease
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public List<Symptom> Symptoms { get; private set; }

    public Disease(string id, string name, List<Symptom> symptoms)
    {
        // CELOWO PUSTE - Czekamy na fazę GREEN.
        // W C# domyślnie stringi będą tutaj równe null, a bool będzie false.
    }
}