using System.Collections;
using UnityEngine;



// This script is attached to the metal door object in the game. It allows the player to interact with the door by pressing the "E" key when they are close enough to it. When the player interacts with the door, it triggers a coroutine that temporarily switches the player's camera to a different view for 3 seconds before switching back to the original camera. This can be used to create a cinematic effect when the player opens the door.
public class MetalDoor : MonoBehaviour
{
    [SerializeField] bool canOpen;
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject theCam;

    void Update()
    {
        if (canOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(OpeningDoor());
            }
        }
    }

    void OnMouseOver()
    {
        if (PlayerCasting.distanceFromTarget < 5)
        {
            canOpen = true;
            UIController.actionText = "Open Door";
            UIController.commandText = "Open";
            UIController.uiActive = true;
        }
        else
        {
            canOpen = false;
            UIController.actionText = "";
            UIController.commandText = "";
            UIController.uiActive = false;
        }
    }

    void OnMouseExit()
    {
        canOpen = false;
        UIController.actionText = "";
        UIController.commandText = "";
        UIController.uiActive = false;
    }

    IEnumerator OpeningDoor()
    {
        theCam.SetActive(true);
        thePlayer.SetActive(false);
        yield return new WaitForSeconds(3);
        thePlayer.SetActive(true);
        theCam.SetActive(false);
    }

}