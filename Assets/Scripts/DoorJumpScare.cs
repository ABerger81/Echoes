using UnityEngine;

// This script is attached to a door object in the game that is meant to be a jump scare. When the player enters the trigger area of the door, it plays a piano scare sound and a door slam sound, while also animating the door to create a jump scare effect. The box colllider on the door is also disabled to prevent the player from triggering the scare multiple times.

public class DoorJumpScare : MonoBehaviour
{
    [SerializeField] AudioSource pianoScare;
    [SerializeField] AudioSource doorSlam;
    [SerializeField] GameObject theDoor;

    void OnTriggerEnter(Collider other)
    {
        theDoor.GetComponent<Animator>().Play("JumpAnimDoor");
        pianoScare.Play();
        doorSlam.Play();
        this.GetComponent<BoxCollider>().enabled = false;
    }
}
