// // GameBootstrapper.cs
// using UnityEngine;

// public class GameBootstrapper : MonoBehaviour {
//     [SerializeField] private MedicalTestManager testManager; // Referencja w inspektorze

//     private void Awake() {
//         // 1. Tworzymy konkretną implementację bazy danych
//         IDataRepository repository = new JsonDataRepository();
        
//         // 2. Ładujemy dane
//         repository.LoadStaticData();
        
//         // 3. Wstrzykujemy zależność do managera
//         // Manager nie wie, że to JSON – wie tylko, że to IDataRepository
//         testManager.Initialize(repository);
        
//         Debug.Log("System zainicjalizowany pomyślnie.");
//     }
// }