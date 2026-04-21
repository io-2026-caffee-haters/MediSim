public interface IMedicalTest
{
    string Name { get; }
    float Duration { get; }
    MedicalTestResult PerformOn(Patient patient);  
}