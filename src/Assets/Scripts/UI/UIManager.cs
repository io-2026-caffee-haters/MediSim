using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    // Stos przechowujący historię otwartych okien
    private Stack<BaseView> _activeViews = new Stack<BaseView>();

    [Header("Dostępne widoki")] //Dodawanie widoków w Inspektorze
    [SerializeField] private BaseView startingView; // np. menu

    private void Start(){
        if (startingView != null) { OpenView(startingView); } // Widok, który pojawi się po uruchomieniu sceny
    }

    // Otwiera nowe okno i dodaje je na górę stosu
    public void OpenView(BaseView view) {
        view.Initialize(this); // Łączenie widoku z managerem
        view.Show();
        _activeViews.Push(view);
    }

    // Zamyka ostatnio otwarte okno (zdejmuje je ze stosu)
    public void CloseCurrentView() {
        if (_activeViews.Count > 0) {
            BaseView viewToClose = _activeViews.Pop();
            viewToClose.Hide();
        }
    }

}