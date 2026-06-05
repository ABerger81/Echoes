using UnityEngine;


// This script is attached to the metal door object in the game. It handles the player's interaction with the door by updating the ui elements to indicate that the player can open the door when they are looking at it. When the player looks away from the door the ui elements are hidden.
public class MetalDoor : MonoBehaviour
{
    void OnMouseOver()
    {
        if (PlayerCasting.distanceFromTarget < 5)
        {
            UIController.actionText = "Open Door";
            UIController.commandText = "Open";
            UIController.uiActive = true;
        }
        else
        {
            UIController.actionText = "";
            UIController.commandText = "";
            UIController.uiActive = false;
        }
    }

    void OnMouseExit()
    {
        UIController.actionText = "";
        UIController.commandText = "";
        UIController.uiActive = false;
    }
}
