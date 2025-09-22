using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableImageHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag Settings")]
    public float returnSpeed = 10f;
    public bool returnToStart = true;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private bool isDragging = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        startPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Smooth return to start position if enabled
        if (!isDragging && returnToStart && rectTransform.anchoredPosition != startPosition)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition,
                startPosition,
                returnSpeed * Time.deltaTime
            );

            // Snap to position when close enough
            if (Vector2.Distance(rectTransform.anchoredPosition, startPosition) < 1f)
            {
                rectTransform.anchoredPosition = startPosition;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        // Bring to front
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform != null && canvas != null)
        {
            // Convert screen position to local position in canvas
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out localPoint
            );

            rectTransform.position = canvas.transform.TransformPoint(localPoint);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        // Check what's under the dropped position
        CheckDropTarget();
    }

    private void CheckDropTarget()
    {
        // Create a pointer event for the current position
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // Raycast to find what's under the cursor
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        bool foundValidTarget = false;

        foreach (var result in results)
        {
            GameObject hitObject = result.gameObject;

            // Skip self
            if (hitObject == gameObject)
                continue;

            // Check for garden tag
            if (hitObject.CompareTag("garden"))
            {
                HandleGardenDrop(hitObject);
                foundValidTarget = true;
                break;
            }
            // Check for money tag
            else if (hitObject.CompareTag("money"))
            {
                HandleMoneyDrop(hitObject);
                foundValidTarget = true;
                break;
            }
        }

        // If no valid target found and return is enabled, return to start
        if (!foundValidTarget && returnToStart)
        {
            // Already handled by Update lerp
        }
    }

    private void HandleGardenDrop(GameObject gardenObject)
    {
        Debug.Log($"Dropped on garden: {gardenObject.name}");

        // Add your garden-specific logic here
        // Example: Change color, trigger animation, etc.
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = Color.green;
        }
        SaveSystem.Instance.PlantCarrot();
        // Optional: Snap to garden position
        // rectTransform.position = gardenObject.transform.position;

        // Remove this component after handling
        RemoveHandler();
    }

    private void HandleMoneyDrop(GameObject moneyObject)
    {
        Debug.Log($"Dropped on money: {moneyObject.name}");

        // Add your money-specific logic here
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = Color.yellow;
        }

        // Optional: Add money value, play sound, etc.

        // Remove this component after handling
        RemoveHandler();
    }

    private void RemoveHandler()
    {
        // Optional: Add delay before removal
        // Invoke(nameof(DestroyHandler), 0.5f);

        DestroyHandler();
    }

    private void DestroyHandler()
    {
        // Remove this component from the game object
        Destroy(this.gameObject);

        // Optional: Destroy the entire game object instead
        // Destroy(gameObject);
    }

    // Public method to manually trigger removal
    public void RemoveDraggableHandler()
    {
        RemoveHandler();
    }

    // Clean up when component is destroyed
    void OnDestroy()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
}