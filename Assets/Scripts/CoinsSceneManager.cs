using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class CoinsSceneManager : MonoBehaviour
{
    public Person Person;

    public TMP_Text NameText, CoinsText;

    public GameObject AddCoinGO;

    private void Awake()
    {
        Person = SaveSystem.Instance.CurActivePerson;

        // !!!!!!!!!!!!!!!!!!!!!!!!!
        // Check if first launch and disable tutorial GO if not
        // !!!!!!!!!!!!!!!!!!!!!!!!!
    }

    private void Start()
    {
        NameText.text = Person.name;
        CoinsText.text = Person.Coins.ToString();
    }

    public TMP_Text AddCoinsValueText;
    public TMP_InputField InterestInput, GrowthTImeInput;
    public int AddCoinsVal;

    public void AddCoinsBtn() // !!!!!!!!!!!!!!!!!! setup interest rate and growth time here
    {
        AddCoinGO.gameObject.SetActive(true);
        AddCoinsVal = 0;
        AddCoinsValueText.text = AddCoinsVal.ToString() ;
        InterestInput.text = SaveSystem.Instance.CurActivePerson.InterestRate.ToString();
        GrowthTImeInput.text = SaveSystem.Instance.CurActivePerson.GrowthInMin.ToString();
    }

    public void IncDecAddCoins(int index)
    {
        if(index == 0)
        {
            AddCoinsVal--;
            if(AddCoinsVal < 0)
            {
                AddCoinsVal = 0;
            }
            AddCoinsValueText.text = AddCoinsVal.ToString();
        } else
        {
            AddCoinsVal++;
            AddCoinsValueText.text = AddCoinsVal.ToString();
        }
    }

    public TMP_InputField GetInterestInput()
    {
        return InterestInput;
    }

    public void SubmitNewCoins()
    {
        SaveSystem.Instance.CurActivePerson.Coins += AddCoinsVal;
        int interest = int.Parse(InterestInput.text);
        if(interest <= 0)
        {
            interest = 1;
        }
        int growth = int.Parse(GrowthTImeInput.text);
        if(growth <= 0)
        {
            growth = 960;
        }
        SaveSystem.Instance.CurActivePerson.InterestRate = interest;
        SaveSystem.Instance.CurActivePerson.GrowthInMin = growth;

        AddCoinsVal = 0;
        CoinsText.text = Person.Coins.ToString();
        AddCoinGO.gameObject.SetActive(false);
    }

    [Header("Spawn Settings")]
    public GameObject imagePrefab; // Assign your Image prefab in the inspector
    public float maxOffset = 50f; // Maximum offset in pixels
    public Transform canvasTransform; // Assign your Canvas transform

    public void SpawnCoinBtn()
    {
        SpawnImageAtCenter();
        SaveSystem.Instance.CurActivePerson.Coins--;
        CoinsText.text = Person.Coins.ToString(); // call this after manipulations
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void SpawnImageAtCenter()
    {
        if (imagePrefab == null || canvasTransform == null)
        {
            Debug.LogError("Prefab or Canvas not assigned!");
            return;
        }

        // Instantiate the image
        GameObject spawnedImage = Instantiate(imagePrefab, canvasTransform);

        // Get the RectTransform component
        RectTransform rectTransform = spawnedImage.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            // Center the image first
            rectTransform.anchoredPosition = Vector2.zero;

            // Apply random offset
            Vector2 randomOffset = new Vector2(
                Random.Range(-maxOffset, maxOffset),
                Random.Range(-maxOffset, maxOffset)
            );

            rectTransform.anchoredPosition += randomOffset;

            Debug.Log($"Spawned image at position: {rectTransform.anchoredPosition}");
        }
    }
    public void GardenBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
