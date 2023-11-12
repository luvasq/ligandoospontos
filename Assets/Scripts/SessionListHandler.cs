using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionListHandler : MonoBehaviour
{
    public TMP_Dropdown patientDropdown;
    public TMP_Dropdown activityDropdown;
    public ScrollRect itemsContainer;
    public GameObject itemPrefab;

    void Start()
    {
        Debug.Log("rodou start da lista");
        // Load and display items based on default folder selections

        // LoadItems();
    }

    public void LoadItems()
    {
        Debug.Log("rodou LoadItems");
        // Clear existing items
        ClearTable();

        foreach (var options in patientDropdown.options)
        {
            Debug.Log($"option: {options.text}");
        }
        Debug.Log(
            $"activityOptions = {activityDropdown.options}, patientOptions = {patientDropdown.options}"
        );

        string activityName = activityDropdown.options[activityDropdown.value].text;

        string patientName = patientDropdown.options[patientDropdown.value].text;
        Debug.Log($"activityName = {activityName}, patientName = {patientName}");

        // Read and parse JSON files
        List<SessionData> items = LoadAndParseJsons(patientName, activityName);

        // Instantiate clickable items
        int counter = 0;
        foreach (SessionData item in items)
        {
            GameObject listItem = Instantiate(itemPrefab, itemsContainer.content);
            // Set the position of the new slider based on its index
            RectTransform sliderTransform = listItem.GetComponent<RectTransform>();
            Vector2 newPosition = new Vector2(
                sliderTransform.anchoredPosition.x,
                (-counter) * 10 + 140
            ); // Adjust Y position based on index
            sliderTransform.anchoredPosition = newPosition;
            counter++;

            TextMeshProUGUI dateText = listItem
                .transform
                .Find("DateText")
                ?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI diffText = listItem
                .transform
                .Find("DiffText")
                ?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = listItem
                .transform
                .Find("ScoreText")
                ?.GetComponent<TextMeshProUGUI>();

            dateText.SetText(item.timestamp);
            diffText.SetText(Math.Round(item.difficultyFactor).ToString());
            scoreText.SetText(Math.Round(item.score).ToString());
            // Add click event
            listItem
                .GetComponentInChildren<Button>()
                ?.onClick
                .AddListener(() => OnButtonClick(item));
        }
    }

    List<SessionData> LoadAndParseJsons(string patientName, string activityName)
    {
        Debug.Log("rodou loadandParse");

        List<SessionData> jsonFiles = new List<SessionData>();
        string path = Path.Combine(Application.persistentDataPath, patientName, activityName);
        Debug.Log($"path: {path}");

        string[] fileNames = Directory.GetFiles(path);

        foreach (string fileName in fileNames)
        {
            Debug.Log($"filename: {fileName}");
            jsonFiles.Add(
                JsonConvert.DeserializeObject<SessionData>(
                    File.ReadAllText(Path.Combine(path, fileName))
                )
            );
        }

        return jsonFiles;
    }

    void OnButtonClick(SessionData item)
    {
        // Handle item click (expand/collapse details, show popup, etc.)
        Debug.Log($"Item Clicked: {item.timestamp}");
    }

    public void ClearTable()
    {
        // Destroy existing clickable items
        foreach (Transform child in itemsContainer.content)
        {
            Destroy(child.gameObject);
        }
    }

    // Other methods for reading JSON, parsing data, etc.
}
