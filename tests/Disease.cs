using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Disease
{
    public int id;
    public string nazwa;
    public List<int> symptomIds;
}