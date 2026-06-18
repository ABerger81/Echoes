using UnityEngine;


// This script is attached to the box object in the game. It handles the player's interaction with the box by making it disappear when the player enters the trigger collider attached to the box. This is used to create a jump scare effect when the player approaches the box.

public class BoxScare : MonoBehaviour
{
    public GameObject boxHolder;

    void OnTriggerEnter(Collider other)
    {
        boxHolder.SetActive(false);
    }
}
