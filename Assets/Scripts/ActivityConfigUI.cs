using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivityConfigUI : MonoBehaviour
{
    public Slider sliderPrefab;
    public RectTransform sliderContainer;
    public TextMeshProUGUI activityNameText;

    public TextMeshProUGUI difficultyFactorText;

    public TMP_Dropdown selectedActivityDropdown;

    // The loaded configuration data
    public SessionData.DiffDetails activityConfig;
    public float diffFactor = 1f;

    void Start()
    {
        LoadAndDisplayActivityConfig();
    }

    private bool isLoading;

    // Use this method to load and display the configuration for a specific activity
    public void LoadAndDisplayActivityConfig()
    {
        if (!isLoading)
        {
            isLoading = true;

            // Clear existing sliders
            ClearSliders();

            string activityName = selectedActivityDropdown
                .options[selectedActivityDropdown.value]
                .text;
            // Load the configuration for the specified activity
            activityConfig = LoadActivityConfig(activityName);

            // Check if the configuration was loaded successfully
            if (activityConfig != null)
            {
                // Display the activity name
                activityNameText.text = activityName;

                int counter = 0;
                // Display sliders for each key-value pair in the configuration
                foreach (var kvp in activityConfig)
                {
                    CreateSlider(kvp.Key, kvp.Value, counter);
                    counter++;
                }
            }
            isLoading = false;
        }
    }

    // Use this method to load the configuration for a specific activity
    private SessionData.DiffDetails LoadActivityConfig(string activityName)
    {
        string activitiesPath = Path.Combine(Application.persistentDataPath, "_activities");

        if (!Directory.Exists(activitiesPath))
        {
            Directory.CreateDirectory(activitiesPath);
        }

        string activityFilePath = Path.Combine(activitiesPath, activityName + ".json");

        // Check if the activity folder exists
        if (!File.Exists(activityFilePath))
        {
            SessionData.DiffDetails defaults;
            if (activityName == "Apertar Botão")
            {
                defaults = new SessionData.DiffDetails
                {
                    { "Altura", 2.3f },
                    { "Distância", 1.7f }
                };
            }
            else if (activityName == "Abrir Porta")
            {
                defaults = new SessionData.DiffDetails { { "Grau", 2.3f }, { "Distância", 1.7f } };
            }
            else if (activityName == "Jogar bola no lixo")
            {
                defaults = new SessionData.DiffDetails { { "Distância do lixo", 1f } };
            }
            else
            {
                defaults = new SessionData.DiffDetails { };
            }

            string jsonData = JsonConvert.SerializeObject(defaults);
            File.WriteAllText(activityFilePath, jsonData);
        }

        // Read the JSON content from the file
        string jsonContent = File.ReadAllText(activityFilePath);

        // Deserialize the JSON content into a dictionary
        SessionData.DiffDetails configData = JsonConvert.DeserializeObject<SessionData.DiffDetails>(
            jsonContent
        );

        return configData;
    }

    // Use this method to create a Slider for a key-value pair
    private void CreateSlider(string key, float defaultValue, int index)
    {
        // Instantiate a new slider from the prefab
        Slider newSlider = Instantiate(sliderPrefab, sliderContainer);

        // Set the slider label (key)
        newSlider.GetComponentInChildren<TextMeshProUGUI>().text = key;

        // Set slider properties
        newSlider.minValue = 1.0f;
        newSlider.maxValue = 4.0f; // Set your own max value
        newSlider.value = defaultValue;

        // Set the position of the new slider based on its index
        RectTransform sliderTransform = newSlider.GetComponent<RectTransform>();
        Vector2 newPosition = new Vector2(sliderTransform.anchoredPosition.x, (-index) * 15 + 25); // Adjust Y position based on index
        sliderTransform.anchoredPosition = newPosition;

        TextMeshProUGUI specificTextComponent = newSlider
            .transform
            .Find("ValueText")
            ?.GetComponent<TextMeshProUGUI>();

        if (specificTextComponent != null)
        {
            double roundedValue = Math.Round(defaultValue, 2);
            // Change the text value
            specificTextComponent.SetText(roundedValue.ToString());
            Debug.Log("Text value changed successfully: " + roundedValue);
        }

        difficultyFactorText.SetText(
            "Fator de Dificuldade: x" + Math.Round(CalcDiffFactor(activityConfig), 2)
        );
        // Attach an event listener to handle changes
        newSlider.onValueChanged.AddListener(value => OnSliderValueChanged(newSlider, key, value));
    }

    // Use this method to handle slider value changes
    private void OnSliderValueChanged(Slider slider, string key, float value)
    {
        // Update the value in the configuration dictionary
        if (activityConfig != null && activityConfig.ContainsKey(key))
        {
            activityConfig[key] = value;
        }

        TextMeshProUGUI specificTextComponent = slider
            .transform
            .Find("ValueText")
            ?.GetComponent<TextMeshProUGUI>();

        if (specificTextComponent != null)
        {
            double roundedValue = Math.Round(value, 2);
            // Change the text value
            specificTextComponent.SetText(roundedValue.ToString());
            Debug.Log("Text value changed successfully: " + roundedValue);
        }
        else
        {
            Debug.Log("TextMeshProUGUI component not found on the child GameObject.");
        }

        difficultyFactorText.SetText(
            "Fator de Dificuldade: x" + Math.Round(CalcDiffFactor(activityConfig), 2)
        );
    }

    private void ClearSliders()
    {
        foreach (Transform child in sliderContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public float CalcDiffFactor(SessionData.DiffDetails activityConfig)
    {
        diffFactor = 1f;

        foreach (var kvp in activityConfig)
        {
            diffFactor *= kvp.Value;
        }

        return diffFactor;
    }
}
