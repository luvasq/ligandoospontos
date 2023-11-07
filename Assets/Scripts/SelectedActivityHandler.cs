using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedActivityHandler : MonoBehaviour
{
    public Dropdown dropdown; // Reference to the Dropdown UI element

    public void UpdateSelectedActivity()
    {
        // Get the selected option from the dropdown
        string selectedOption = dropdown.options[dropdown.value].text;

        GlobalDataManager.Instance.selectedActivity = selectedOption;
    }
}
