using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The Tag of the cube that should be placed here (e.g., RedCube)")]
    public string requiredTag;
    

    // Hidden variable to let the manager know if this specific plate is solved
    [HideInInspector]
    public bool isActivated = false;

    private PuzzleManager manager;

    void Start()
    {
        // Find the manager in the scene automatically
        manager = FindObjectOfType<PuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the correct tag
        if (other.CompareTag(requiredTag))
        {
            isActivated = true;
            Debug.Log(gameObject.name + " Activated!");
            manager.CheckForWin(); // Tell manager to check if we won
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the correct cube leaves, deactivate this plate
        if (other.CompareTag(requiredTag))
        {
            isActivated = false;
        }
    }
}