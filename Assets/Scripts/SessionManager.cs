using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    public TMP_Dropdown patientDropdown;
    public TMP_Dropdown activityDropdown;

    public ActivityConfigUI activityConfig;

    private string dataFilePath;

    private float lastStartTime;

    public void Save()
    {
        System.DateTime currentDateTime = System.DateTime.Now;

        string selectedPatient = patientDropdown.options[patientDropdown.value].text;
        string selectedActivity = activityDropdown.options[activityDropdown.value].text;

        float timeTaken = Time.time - lastStartTime;
        Debug.Log($"timeTaken: {timeTaken}");
        SessionData session = new SessionData
        {
            patient = selectedPatient,
            activity = selectedActivity,
            timestamp = currentDateTime.ToString(),
            timeTaken = timeTaken,
            difficultyFactor = activityConfig.diffFactor,
            score = CalcScore(timeTaken, activityConfig.diffFactor),
            difficulty = activityConfig.activityConfig,
        };

        string folderPath = Path.Combine(
            Application.persistentDataPath,
            selectedPatient,
            selectedActivity
        );

        // Create the necessary directories if they do not exist
        Directory.CreateDirectory(folderPath);

        dataFilePath = Path.Combine(
            folderPath,
            currentDateTime.ToString().Replace(' ', '_').Replace('/', '-').Replace(':', '-')
                + ".json"
        );

        string jsonData = JsonConvert.SerializeObject(session);
        File.WriteAllText(dataFilePath, jsonData);
    }

    public void SaveStartTime()
    {
        lastStartTime = Time.time;
        Debug.Log($"lastStartTime: {lastStartTime.ToString()}");
    }

    public float CalcScore(float timeTaken, float diffFactor)
    {
        return diffFactor * 1000 / timeTaken;
    }

    // public SessionData Load()
    // {
    //     string selectedPatient = patientDropdown.options[patientDropdown.value].text;
    //     string selectedActivity = activityDropdown.options[activityDropdown.value].text;

    //     string folderPath = Path.Combine(
    //         Application.persistentDataPath,
    //         selectedPatient,
    //         selectedActivity
    //     );

    //     if (File.Exists(dataFilePath))
    //     {
    //         string jsonData = File.ReadAllText(dataFilePath);
    //         return JsonUtility.FromJson<SessionData>(jsonData);
    //     }
    //     else
    //     {
    //         // Handle the case where the file doesn't exist (no saved data)
    //         return new SessionData(); // You can return default values or handle this as needed
    //     }
    // }
}
