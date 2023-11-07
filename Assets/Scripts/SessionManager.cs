using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        public Dictionary<string, float> difficulty;
    }

    private string dataFilePath;

    public void Save()//SessionData session)
    {
        System.DateTime currentDateTime = System.DateTime.Now;

        string selectedPatient = GlobalDataManager.Instance.selectedPatient;
        string selectedActivity = GlobalDataManager.Instance.selectedActivity;

        SessionData session = new SessionData
        {
            patient = selectedPatient,
            activity = selectedActivity,
            timestamp = System.DateTime.Now.ToString(),
            timeTaken = 30.5f,
            difficultyFactor = 0.8f,
            score = 95.5f,
            difficulty = new Dictionary<string, float>
            {
                { "level1", 2.3f },
                { "level2", 1.7f },
                { "level3", 3.1f }
            },
        };

        
        dataFilePath = Path.Combine(Application.persistentDataPath, selectedPatient, selectedActivity, currentDateTime + ".json");

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
