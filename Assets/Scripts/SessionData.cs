using System.Collections.Generic;

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
    public class DiffDetails : Dictionary<string, float> { }

    public DiffDetails difficulty;
}
