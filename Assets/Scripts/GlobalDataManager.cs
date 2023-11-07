using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
    // Define your global data variables here
    public string selectedPatient;
    public string selectedActivity;

    // Singleton instance reference
    private static GlobalDataManager instance;

    // Public property to access the singleton instance
    public static GlobalDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GlobalDataManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GlobalDataManager");
                    instance = singletonObject.AddComponent<GlobalDataManager>();
                }
            }
            return instance;
        }
    }

    // Optionally, set this instance not to be destroyed when loading new scenes
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
