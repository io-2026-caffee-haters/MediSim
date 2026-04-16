using UnityEngine;
using System.Collections.Generic;

/// Klasa danych o symptomie.

[System.Serializable]
public class Symptom
{

    public int id;
    public string name;
    public bool isVisible; /// Czy widoczny na pacjencie bez badania.

}
