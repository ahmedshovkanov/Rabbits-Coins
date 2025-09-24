using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class CarrotWindowManager : MonoBehaviour
{
    //[Header("Carrot Data")]
    //public List<CarrotData> carrots = new List<CarrotData>();

    public static CarrotWindowManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Spawn Settings")]
    public GameObject carrotPrefab; // Assign your carrot prefab in inspector
    public Transform containerParent; // Assign the parent GameObject with 10 children

    [Header("Row Settings")]
    public int[] firstRowIndices = new int[] { 4, 6, 8 };
    public int[] secondRowIndices = new int[] { 5, 7, 9 };

    void Start()
    {
        if (containerParent == null || carrotPrefab == null)
        {
            Debug.LogError("Container parent or carrot prefab not assigned!");
            return;
        }

        RowsSetup();
        SpawnCarrots();
    }
    public GameObject RowPrefab;
    public Transform RowParent;

    public void HomeBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void BonusGameBtn()
    {
        SceneManager.LoadScene(0);
    }

    private void RowsSetup()
    {
        int carrotsDif = 15 - SaveSystem.Instance.CurActivePerson.carrots.Count;
        for (int i = 0; i < SaveSystem.Instance.CurActivePerson.carrots.Count; i++)
        {
            Instantiate(RowPrefab, RowParent);
        }
        //Debug.LogWarning("rows to spawn " + rowsToSpawn);
        //if (carrotsDif > 0)
        //{
        //    int rowsToSpawn = DivideWithRemainder(carrotsDif, 3);
        //    Debug.LogWarning("rows to spawn " + rowsToSpawn);
        //    for (int i = 0; i < rowsToSpawn; i++)
        //    {
        //        Instantiate(RowPrefab, RowParent);
        //        Instantiate(RowPrefab, RowParent);
        //    }
        //}
    }

    public int DivideWithRemainder(int number, int divisor)
    {
        int result = 0;

        int baseValue = number / divisor; 
        int remainder = number % divisor;

        if (remainder > 0)
        {
            result++;
        }

        return result;
    }

    public void SpawnCarrots()
    {
        // Clear existing carrots first
        //ClearExistingCarrots();

        if (SaveSystem.Instance.CurActivePerson.carrots.Count == 0)
        {
            Debug.LogWarning("No carrot data to spawn!");
            return;
        }
        int count = SaveSystem.Instance.CurActivePerson.carrots.Count;
        int row = 0;
        for (int i = 0; i < count; i += 3)
        {
            if (row % 2 == 0)
            {
                if (i < count)
                {
                    CarrotHandler car = Instantiate(carrotPrefab, containerParent.GetChild(row).GetChild(firstRowIndices[0])).GetComponent<CarrotHandler>();
                    if (car != null)
                    {
                        car.InitHandler(SaveSystem.Instance.CurActivePerson.carrots[i]);
                    }
                }
                if (i + 1 < count)
                {
                    CarrotHandler car = Instantiate(carrotPrefab, containerParent.GetChild(row).GetChild(firstRowIndices[1])).GetComponent<CarrotHandler>();
                    if (car != null)
                    {
                        car.InitHandler(SaveSystem.Instance.CurActivePerson.carrots[i + 1]);
                    }
                }
                if (i + 2 < count)
                {
                    CarrotHandler car = Instantiate(carrotPrefab, containerParent.GetChild(row).GetChild(firstRowIndices[2])).GetComponent<CarrotHandler>();
                    if (car != null)
                    {
                        car.InitHandler(SaveSystem.Instance.CurActivePerson.carrots[i + 2]);
                    }
                }
                Debug.Log($"{row} is even");
                row++;
            }
            else
            {
                if (i < count)
                {
                    CarrotHandler car = Instantiate(carrotPrefab, containerParent.GetChild(row).GetChild(secondRowIndices[0])).GetComponent<CarrotHandler>();
                    if (car != null)
                    {
                        car.InitHandler(SaveSystem.Instance.CurActivePerson.carrots[i]);
                    }
                }
                if (i + 1 < count)
                {
                    CarrotHandler car = Instantiate(carrotPrefab, containerParent.GetChild(row).GetChild(secondRowIndices[1])).GetComponent<CarrotHandler>();
                    if (car != null)
                    {
                        car.InitHandler(SaveSystem.Instance.CurActivePerson.carrots[i + 1]);
                    }
                }
                if (i + 2 < count)
                {
                    CarrotHandler car = Instantiate(carrotPrefab, containerParent.GetChild(row).GetChild(secondRowIndices[2])).GetComponent<CarrotHandler>();
                    if (car != null)
                    {
                        car.InitHandler(SaveSystem.Instance.CurActivePerson.carrots[i + 2]);
                    }
                }
                Debug.Log($"{row} is odd");
                row++;
            }
        }
    }

    private Transform GetTargetParent(int rowCounter, int carrotIndex)
    {
        int patternIndex = carrotIndex % (firstRowIndices.Length + secondRowIndices.Length);
        int actualRowIndex;

        if (patternIndex < firstRowIndices.Length)
        {
            // First row pattern
            actualRowIndex = firstRowIndices[patternIndex] + (rowCounter * 10);
        }
        else
        {
            // Second row pattern
            int secondRowPatternIndex = patternIndex - firstRowIndices.Length;
            actualRowIndex = secondRowIndices[secondRowPatternIndex] + (rowCounter * 10);
        }

        // Check if the calculated index is valid
        if (actualRowIndex < containerParent.childCount)
        {
            return containerParent.GetChild(actualRowIndex);
        }

        Debug.LogWarning($"Invalid parent index: {actualRowIndex}");
        return null;
    }

    private int GetSlotIndex(int rowCounter, int carrotIndex)
    {
        int patternIndex = carrotIndex % (firstRowIndices.Length + secondRowIndices.Length);

        if (patternIndex < firstRowIndices.Length)
        {
            return firstRowIndices[patternIndex] + (rowCounter * 10);
        }
        else
        {
            int secondRowPatternIndex = patternIndex - firstRowIndices.Length;
            return secondRowIndices[secondRowPatternIndex] + (rowCounter * 10);
        }
    }

    private int GetTotalAvailableSlots()
    {
        return containerParent.childCount;
    }

    private void InitializeCarrot(GameObject carrotInstance, CarrotData data)
    {

    }

    public void ClearExistingCarrots()
    {
        // Destroy all carrot instances in all child slots
        foreach (Transform slot in containerParent)
        {
            foreach (Transform child in slot)
            {
                //if (child.CompareTag("Carrot")) // Optional: use tag to identify carrots
                //{
                //    Destroy(child.gameObject);
                //}
            }
        }

        // Alternative: find all carrots in scene and destroy them

    }

    public void AddCarrot(CarrotData newCarrot)
    {
        SaveSystem.Instance.CurActivePerson.carrots.Add(newCarrot);
        SpawnCarrots(); // Respawn all carrots
    }

    public void RemoveCarrot(int index)
    {
        if (index >= 0 && index < SaveSystem.Instance.CurActivePerson.carrots.Count)
        {
            SaveSystem.Instance.CurActivePerson.carrots.RemoveAt(index);
            SpawnCarrots(); // Respawn remaining carrots
        }
    }

    // Method to test the spawning pattern
    [ContextMenu("Test Spawn Pattern")]
    public void TestSpawnPattern()
    {
        // Create test data
        //SaveSystem.Instance.CurActivePerson.carrots.Clear();
        for (int i = 0; i < 12; i++)
        {
            //carrots.Add(new CarrotData($"Test Carrot {i + 1}", Random.Range(3, 8), Random.Range(80f, 200f)));
        }

        SpawnCarrots();
    }

    // Method to visualize the spawn pattern in editor
    void OnDrawGizmosSelected()
    {
        if (containerParent == null) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < containerParent.childCount; i++)
        {
            Transform child = containerParent.GetChild(i);
            Gizmos.DrawWireCube(child.position, Vector3.one * 0.5f);
        }
    }
}