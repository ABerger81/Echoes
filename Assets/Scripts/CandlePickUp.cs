using UnityEngine;


// This script is attached to the candle object in the game. It allows the player to interact with the candle by pressing the "E" key when they are close enough to it. When the player interacts with the candle, it disables the box collider on the candle, making it non-interactable, and hides both the table candle and hand candle game objects. This can be used to simulate the player picking up the candle and adding it to their inventory or using it by holding it in their hand.


public class CandlePickUp : MonoBehaviour
{
    [SerializeField] bool canPick;
    [SerializeField] GameObject textOnScreen;
    [SerializeField] GameObject tableCandle;
    [SerializeField] GameObject handCandle;

    void Update()
    {
        if (canPick == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.GetComponent<BoxCollider>().enabled = false;
                tableCandle.SetActive(false);
                handCandle.SetActive(true);
            }
        }
    }

    void OnMouseOver()
    {
        if (PlayerCasting.distanceFromTarget < 7)
        {
            canPick = true;
            UIController.actionText = "Candle";
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