public interface IMedicalTest
{
    string Name { get; }
    MedicalTestResult PerformOn(Patient patient);  
}