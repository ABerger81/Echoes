using System.Collections;
using UnityEngine;

// This script is attached to the metal door object in the game. It allows the player to interact with the door by pressing the "E" key when they are close enough to it. When the player interacts with the door, it triggers a coroutine that plays a creaking sound and animates the door opening. The box collider on the door is also disabled to allow the player to pass through it after it has been opened.

public class OpenDoor : MonoBehaviour
{
    [SerializeField] bool canOpen;
    [SerializeField] GameObject theDoor;
    [SerializeField] AudioSource creakDoor;

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
        creakDoor.Play();
        theDoor.GetComponent<Animator>().Play("MetalDoorOpen");
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2);
    }
}
