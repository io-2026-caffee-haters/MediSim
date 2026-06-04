using UnityEngine;

public class SaveLoadController : MonoBehaviour
{

    public SaveSystem saveSystem;
    public ScoreTimeController scoreTimeController;
    public MedicalTestManager medicalTestManager;
    public MedicalTestView medicalTestView;
    public PatientSpawner patientSpawner;

    public static bool IsLoadingGame = false;

    void Awake()
    {
        if (PlayerPrefs.GetInt("LoadGameFlag", 0) == 1)
        {
            IsLoadingGame = true;
            
            PlayerPrefs.SetInt("LoadGameFlag", 0);
            PlayerPrefs.Save();
        }
    }

    void Start()
    {
        if (IsLoadingGame)
        {
            ExecuteLoadGame();
        }
    }


    public void ExecuteSaveGame()
    {
        if (saveSystem == null) return;

        SaveData data = new SaveData();

        data.currentScore = scoreTimeController.GetCurrentScore();
        data.remainingTime = scoreTimeController.GetRemainingTime();

        data.playerNotes = (medicalTestView != null && medicalTestView.interviewResult != null) 
            ? medicalTestView.interviewResult.text 
            : "";

        if (medicalTestManager != null && medicalTestManager.currentActivePatient != null && medicalTestManager.currentActivePatient.myDisease != null)
        {
            data.currentDiseaseId = medicalTestManager.currentActivePatient.myDisease.id.ToString();
        }

        if (medicalTestManager != null)
        {
            foreach (var kvp in medicalTestManager.GetCooldowns())
            {
                float timeLeft = kvp.Value - Time.time;
                if (timeLeft > 0)
                {
                    data.cooldownTestIds.Add(kvp.Key);
                    data.cooldownTimes.Add(timeLeft);
                }
            }
        }

        saveSystem.SaveCurrentGame(data);
    }


    public void ExecuteLoadGame()
    {
        if (saveSystem == null) return;

        SaveData data = saveSystem.LoadGame();
        if (data == null) return;

        if (scoreTimeController != null)
        {
            scoreTimeController.RestoreState(data.remainingTime, data.currentScore);
        }

        if (medicalTestView != null && medicalTestView.interviewResult != null)
        {
            medicalTestView.interviewResult.text = data.playerNotes;
        }

        if (medicalTestManager != null)
        {
            medicalTestManager.RestoreCooldowns(data.cooldownTestIds, data.cooldownTimes);
        }

        if (patientSpawner != null && !string.IsNullOrEmpty(data.currentDiseaseId))
        {
            patientSpawner.SpawnPatientFromLoad(data.currentDiseaseId);
        }
    }
}