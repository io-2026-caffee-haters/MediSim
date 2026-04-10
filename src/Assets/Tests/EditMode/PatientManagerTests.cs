using System.Reflection;
using NUnit.Framework;

public class PatientManagerTests
{   
    private PatientManager patientManagerTest;

    [SetUp]
    public void SetUp()
    {
        patientManagerTest = new PatientManager();
    }

    [Test]
    public void SpawnNewPatient_InitializePatient()
    {
        patientManagerTest.SpawnNewPatient();
        Assert.IsNotNull(patientManagerTest.CurrentPatient, "Pacjent powinien zostać zainicjalizowany.");
        Assert.IsNotNull(patientManagerTest.CurrentRecord, "Karta medyczna powinna zostać utworzona.");
    }
}