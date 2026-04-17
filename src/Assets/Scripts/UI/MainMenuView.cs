using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuView : BaseView{
    
    [Header("Referencje przycisków")]
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _exitGameButtin;

    public void OnNewGameClicked(){   
        //Tworzenie nowej gry
        Debug.Log("MainMenu: Start nowa gra");
        SceneManager.LoadScene("Clinic"); 
    }

    public void OnLoadGameClicked(){   
        Debug.Log("MainMenu: Próba wczytania gry (to be continued :)"); 
    }

    public void OnExitClicked(){
        Debug.Log("Zamykanie gry...");
    
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // Zamyka gre
        #endif
    }
}