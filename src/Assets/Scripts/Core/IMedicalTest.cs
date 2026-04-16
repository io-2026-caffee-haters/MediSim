/// Interfejs podający standard dla badania medycznego.
/// Wymusza podanie nazwy i wykonanie metody na pacjencie.

public interface IMedicalTest
{

    /// Przechowuje nazwe badania.
    string Name { get; set; }

    /// Wykonuje badanie na pacjencie.
    MedicalTestResult PerformOn(Patient patient);
    
}