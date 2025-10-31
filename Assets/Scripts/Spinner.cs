using UnityEngine;

public class Spinner : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Space rotationSpace = Space.Self;

    [Header("Options")]
    [SerializeField] private bool startOnEnable = true;
    [SerializeField] private bool useUnscaledTime = false;

    private bool isRotating = false;

    void OnEnable()
    {
        if (startOnEnable)
        {
            StartRotation();
        }
    }

    void OnDisable()
    {
        StopRotation();
    }

    void Update()
    {
        if (isRotating)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        float rotationAmount = rotationSpeed * deltaTime;

        transform.Rotate(rotationAxis, rotationAmount, rotationSpace);
    }

    /// <summary>
    /// Start the rotation
    /// </summary>
    public void StartRotation()
    {
        isRotating = true;
    }

    /// <summary>
    /// Stop the rotation
    /// </summary>
    public void StopRotation()
    {
        isRotating = false;
    }

    /// <summary>
    /// Set rotation speed during runtime
    /// </summary>
    public void SetRotationSpeed(float newSpeed)
    {
        rotationSpeed = newSpeed;
    }

    /// <summary>
    /// Set rotation axis during runtime
    /// </summary>
    public void SetRotationAxis(Vector3 newAxis)
    {
        rotationAxis = newAxis.normalized;
    }

    /// <summary>
    /// Toggle rotation on/off
    /// </summary>
    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }
}