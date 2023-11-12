using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    public TMP_Dropdown patientDropdown;
    public TMP_Dropdown activityDropdown;

    private string dataFilePath;

    public void Save()
    {
        System.DateTime currentDateTime = System.DateTime.Now;

        string selectedPatient = patientDropdown.options[patientDropdown.value].text;
        string selectedActivity = activityDropdown.options[activityDropdown.value].text;

        SessionData session = new SessionData
        {
            patient = selectedPatient,
            activity = selectedActivity,
            timestamp = currentDateTime.ToString(),
            timeTaken = 30.5f,
            difficultyFactor = 0.8f,
            score = 95.5f,
            difficulty = new SessionData.DiffDetails { { "Altura", 2.3f }, { "Distância", 1.7f } },
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

    public SessionData Load()
    {
        string selectedPatient = patientDropdown.options[patientDropdown.value].text;
        string selectedActivity = activityDropdown.options[activityDropdown.value].text;

        string folderPath = Path.Combine(
            Application.persistentDataPath,
            selectedPatient,
            selectedActivity
        );

        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            return JsonUtility.FromJson<SessionData>(jsonData);
        }
        else
        {
            // Handle the case where the file doesn't exist (no saved data)
            return new SessionData(); // You can return default values or handle this as needed
        }
    }
}
