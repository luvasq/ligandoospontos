using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class SessionManager : MonoBehaviour
{
    [System.Serializable]
    public class SessionData
    {
        public string patient;
        public string activity;
        public string timestamp;
        public float timeTaken;
        public float difficultyFactor;
        public float score;
        [System.Serializable]
        public class DiffDetails: Dictionary<string,float> { }
        public DiffDetails difficulty;
    }

    public TMP_Dropdown patientDropdown;
    public TMP_Dropdown activityDropdown;

    private string dataFilePath;

    public void Save()//SessionData session)
    {
        System.DateTime currentDateTime = System.DateTime.Now;

        string selectedPatient = patientDropdown.options[patientDropdown.value].text;
        string selectedActivity = activityDropdown.options[activityDropdown.value].text;;
        Debug.Log("selectedPatient: " + selectedPatient);
        Debug.Log("selectedActivity: " + selectedActivity);
        SessionData session = new SessionData
        {
            patient = selectedPatient,
            activity = selectedActivity,
            timestamp = System.DateTime.Now.ToString(),
            timeTaken = 30.5f,
            difficultyFactor = 0.8f,
            score = 95.5f,
            difficulty = new SessionData.DiffDetails
            {
                { "level1", 2.3f },
                { "level2", 1.7f },
                { "level3", 3.1f }
            },
        };

        
        dataFilePath = Path.Combine(Application.persistentDataPath, "abc.json");
        Debug.Log("dataFilePath: " + dataFilePath);

        string jsonData = JsonUtility.ToJson(session);
        File.WriteAllText(dataFilePath, jsonData);
    }

    public SessionData Load()
    {
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
