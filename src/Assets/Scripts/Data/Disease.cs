using UnityEngine;
using System.Collections.Generic;

/// Klasa danych o chorobie.

[System.Serializable]
public class Disease
{

    public int id;
    public string name;
    public List<int> symptomIds;

}