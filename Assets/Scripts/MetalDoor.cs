using UnityEngine;

public class MetalDoor : MonoBehaviour
{
    void OnMouseOver()
    {
        UIController.actionText = "Open Door";
        UIController.commandText = "Open";
        UIController.uiActive = true;
    }

    void OnMouseExit()
    {
        UIController.actionText = "";
        UIController.commandText = "";
        UIController.uiActive = false;
    }
}
