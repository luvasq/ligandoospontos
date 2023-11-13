using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientDropdownListHandler : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public void UpdateDropdownOptions()
    {
        // Get all directories (folders) inside the persistent data folder
        string[] directories = Directory.GetDirectories(Application.persistentDataPath);

        // Clear existing options in the dropdown
        dropdown.ClearOptions();

        // Create a list to store dropdown options
        List<string> options = new List<string>();

        // Add each folder name to the list
        foreach (string directory in directories)
        {
            string folderName = Path.GetFileName(directory);
            if (folderName != "_activities" && folderName != "il2cpp")
            {
                options.Add(folderName);
            }
        }

        // Set the dropdown options
        dropdown.AddOptions(options);
    }

    void Start()
    {
        UpdateDropdownOptions();
        Debug.Log("finalizou start do dropbox");
    }
}
