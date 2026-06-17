using UnityEngine;
using UnityEngine.UI;

// Te dwie linijki sprawiają, że Unity samo doda potrzebne komponenty!
[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public class AutoFrontWindow : MonoBehaviour
{
    private Canvas _myCanvas;
    
    private static int _globalTopLayer = 10;

    private void Awake()
    {
        _myCanvas = GetComponent<Canvas>();
        _myCanvas.overrideSorting = true;
    }

    private void OnEnable()
    {

        if (_myCanvas != null)
        {
            _globalTopLayer++;
            _myCanvas.sortingOrder = _globalTopLayer;
        }
    }
}