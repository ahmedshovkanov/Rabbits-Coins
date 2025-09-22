using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private string savePath;
    public GameData currentGameData;

    //public List<Person> People => currentGameData.people;
    //public List<CarrotData> Carrots => currentGameData.carrots;

    //public List<Person> people = new List<Person>();
    public GameObject buttonPrefab; // Assign your button prefab in the inspector
    public Transform buttonParent;

    private List<GameObject> instantiatedButtons = new List<GameObject>();

    public void PlantCarrot()
    {
        CarrotData car = new CarrotData(SaveSystem.Instance.CurActivePerson.GrowthInMin / 60, DateTime.Now);
        SaveSystem.Instance.CurActivePerson.carrots.Add(car);

        for (int i = 0; i < SaveSystem.Instance.CurActivePerson.InterestRate; i++)
        {
            CarrotData car_i = new CarrotData(SaveSystem.Instance.CurActivePerson.GrowthInMin / 60, DateTime.Now);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savesgame.json");
        currentGameData = new GameData();
        LoadGame();
        CreatePersonButtons();
    }

    public void SaveGame()
    {
        try
        {
            // Update timestamp
            //currentGameData.lastSaveTime = DateTime.Now.ToString();

            // Convert to JSON
            string jsonData = JsonUtility.ToJson(currentGameData, true);

            // Save to file
            File.WriteAllText(savePath, jsonData);

            Debug.Log("Game saved successfully to: " + savePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving game: " + e.Message);
        }
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string jsonData = File.ReadAllText(savePath);
                currentGameData = JsonUtility.FromJson<GameData>(jsonData);

                if (currentGameData.people == null)
                    currentGameData.people = new List<Person>();
                //if (currentGameData.carrots == null)
                 //   currentGameData.carrots = new List<CarrotData>();

                Debug.Log("Game loaded successfully from: " + savePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading game: " + e.Message);
                currentGameData = new GameData();
            }
        }
        else
        {
            Debug.Log("No save file found. Creating new game data.");
            currentGameData = new GameData();
        }
    }

    // Person management
    public void AddPerson(Person person)
    {
        currentGameData.people.Add(person);
    }

    public void RemovePerson(Person person)
    {
        currentGameData.people.Remove(person);
    }

    public void ClearAllPeople()
    {
        currentGameData.people.Clear();
    }

    // Carrot management
    //public void AddCarrot(int row, float timeLeftToGrow, Vector3 position)
    //{
    //    var carrotData = new CarrotData
    //    {
    //        row = row,
    //        timeLeftToGrow = timeLeftToGrow,
    //        position = new Vector3Serializable(position),
    //        isGrowing = timeLeftToGrow > 0
    //    };

    //    currentGameData.carrots.Add(carrotData);
    //}

    //public void UpdateCarrot(int index, float timeLeftToGrow, Vector3 position)
    //{
    //    if (index >= 0 && index < currentGameData.carrots.Count)
    //    {
    //        currentGameData.carrots[index].timeLeftToGrow = timeLeftToGrow;
    //        currentGameData.carrots[index].position = new Vector3Serializable(position);
    //        currentGameData.carrots[index].isGrowing = timeLeftToGrow > 0;
    //    }
    //}

    //public void RemoveCarrot(int index)
    //{
    //    if (index >= 0 && index < currentGameData.carrots.Count)
    //    {
    //        currentGameData.carrots.RemoveAt(index);
    //    }
    //}

    //public void ClearAllCarrots()
    //{
    //    currentGameData.carrots.Clear();
    //}

    // Utility methods
    public bool HasSaveData()
    {
        return File.Exists(savePath);
    }

    public void DeleteSaveData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            currentGameData = new GameData();
            Debug.Log("Save data deleted.");
        }
    }

    public string GetSavePath()
    {
        return savePath;
    }

    // Auto-save on application quit/pause
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }

    void CreatePersonButtons()
    {
        // Clear existing buttons if any
        ClearButtons();

        // Create a button for each person in the list
        for (int i = 0; i < currentGameData.people.Count; i++)
        {
            // Instantiate the button prefab
            GameObject buttonObject = Instantiate(buttonPrefab, buttonParent);
            instantiatedButtons.Add(buttonObject);

            // Get the Button component
            Button button = buttonObject.GetComponent<Button>();

            // Set button text (optional - if your prefab has a Text child)
            var buttonText = buttonObject.transform.GetChild(0).GetComponent<TMP_Text>();
            if (buttonText != null && currentGameData.people[i] != null)   // remove this checks????????????????????????
            {
                buttonText.text = currentGameData.people[i].name; // Assuming Person class has a 'name' property
                buttonObject.transform.GetChild(1).GetComponent<TMP_Text>().text = currentGameData.people[i].Coins.ToString();
            }

            // Add click listener with the current index
            int index = i; // Capture the index in a local variable for the closure
            button.onClick.AddListener(() => OnPersonButtonClicked(index));
        }
    }

    void OnPersonButtonClicked(int personIndex)
    {
        // Handle the button click with the person index
        Debug.Log($"Button clicked for person at index: {personIndex}");

        // You can access the person data like this:
        if (personIndex >= 0 && personIndex < currentGameData.people.Count)
        {
            Person selectedPerson = currentGameData.people[personIndex];
            Debug.Log($"Selected person: {selectedPerson.name}");

            // Call your method here with the person data
            HandlePersonSelection(selectedPerson, personIndex);
        }
    }

    public Person CurActivePerson;

    void HandlePersonSelection(Person person, int index)
    {
        CurActivePerson = person;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ClearButtons()
    {
        // Destroy all instantiated buttons
        foreach (GameObject button in instantiatedButtons)
        {
            if (button != null)
            {
                Destroy(button);
            }
        }
        instantiatedButtons.Clear();
    }

    // Optional: Call this if you need to refresh buttons when the list changes
    public void RefreshButtons()
    {
        CreatePersonButtons();
    }
}
