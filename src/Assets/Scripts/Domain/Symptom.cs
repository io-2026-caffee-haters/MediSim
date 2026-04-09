public class Symptom
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public bool IsVisibleToNakedEye { get; private set; }

    public Symptom(string id, string name, bool isVisible)
    {
        // CELOWO PUSTE - Czekamy na fazę GREEN.
        // W C# domyślnie stringi będą tutaj równe null, a bool będzie false.
    }
}