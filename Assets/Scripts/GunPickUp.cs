using UnityEngine;


// This script is attached to the gun pickup object in the game. It allows the player to pick up the gun when they are close enough to it and press the "E" key. When the player picks up the gun, it disables the box collider on the gun pickup object, activates the hand gun model, and deactivates the table gun model. Additionally, it updates the UI to show that the player can pick up the gun when they are close enough to it, and hides the UI when they are not close enough.

public class GunPickUp : MonoBehaviour
{
    [SerializeField] bool canPick;
    [SerializeField] GameObject textOnScreen;
    [SerializeField] GameObject tableCandle;
    [SerializeField] GameObject handCandle;
    [SerializeField] GameObject handGun;
    [SerializeField] GameObject tableGun;

    void Update()
    {
        if (canPick == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.GetComponent<BoxCollider>().enabled = false;
                tableCandle.SetActive(true);
                handCandle.SetActive(false);
                tableGun.SetActive(false);
                handGun.SetActive(true);
            }
        }
    }

    void OnMouseOver()
    {
        if (PlayerCasting.distanceFromTarget < 5)
        {
            canPick = true;
            UIController.actionText = "Hand Gun";
            UIController.commandText = "Pick Up";
            UIController.uiActive = true;
        }
        else
        {
            canPick = false;
            UIController.actionText = "";
            UIController.commandText = "";
            UIController.uiActive = false;
        }
    }

    void OnMouseExit()
    {
        canPick = false;
        UIController.actionText = "";
        UIController.commandText = "";
        UIController.uiActive = false;
    }
}