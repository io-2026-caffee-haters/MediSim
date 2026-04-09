public interface IMedicalTest
{
    string Name { get; }
    float TimeCost { get; } // Dodane: ile czasu zajmuje test
    MedicalTestResult PerformOn(Patient patient);
}