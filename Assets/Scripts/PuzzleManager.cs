using UnityEngine;
using TMPro; // Include this if you want to change the text on the sign

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Components")]
    public PressurePlate[] plates; // Drag all 3 black planes here

    [Header("Win Effects")]
    public GameObject[] Objects;
 

    public void CheckForWin()
    {
        // Loop through all plates to see if any are FALSE
        foreach (PressurePlate plate in plates)
        {
            if (plate.isActivated == false)
            {
                return; // Stop checking, the puzzle isn't done yet
            }
        }

        // If we didn't return above, that means ALL plates are true!
        WinGame();
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");

        // Example: Change the text on the wall
        
        // Example: Play a particle effect
        if (Objects != null)
        {
           foreach (GameObject obj in Objects)
           {
               obj.SetActive(true); 
            }
        }
    }
}