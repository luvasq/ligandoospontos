using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class AddPatientHandler : MonoBehaviour
{
    public TMP_InputField patientNameInputField;

    // Start is called before the first frame update
    public void CreateNewPatient()
    {
        // Get the name of the new folder from the input field
        string newFolderName = patientNameInputField.text;

        if (string.IsNullOrEmpty(newFolderName))
        {
            Debug.LogWarning("Folder name cannot be empty!");
            return;
        }

        // Combine the persistent data path with the new folder name to get the full path
        string newFolderPath = Path.Combine(Application.persistentDataPath, newFolderName);

        // Check if the folder already exists
        if (Directory.Exists(newFolderPath))
        {
            Debug.LogWarning("Folder already exists!");
            return;
        }

        // Create the new folder
        Directory.CreateDirectory(newFolderPath);

        // Optionally, clear the input field after creating the folder
        patientNameInputField.text = "";

        Debug.Log("Folder created: " + newFolderPath);
    }
}
