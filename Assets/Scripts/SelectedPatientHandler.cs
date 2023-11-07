using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedPatientHandler : MonoBehaviour
{
    public Dropdown dropdown; // Reference to the Dropdown UI element

    public void UpdateSelectedPatient()
    {
        // Get the selected option from the dropdown
        string selectedOption = dropdown.options[dropdown.value].text;

        GlobalDataManager.Instance.selectedPatient = selectedOption;
    }
}
