using UnityEngine;


// This script is attached to the door object in the game. It allows the player to trigger a jump scare when they enter the trigger area of the door. When the player enters the trigger, it plays a piano scare sound and a door slam sound, and it also plays an animation on the door to make it look like it's slamming open. Additionally, it enables the gun pickup object, allowing the player to interact with it after the jump scare has been triggered. Finally, it disables the box collider on the door to prevent the jump scare from being triggered multiple times.

public class DoorJumpScare : MonoBehaviour
{
    [SerializeField] AudioSource pianoScare;
    [SerializeField] AudioSource doorSlam;
    [SerializeField] GameObject theDoor;
    [SerializeField] GameObject gunPickup;

    void OnTriggerEnter(Collider other)
    {
        theDoor.GetComponent<Animator>().Play("JumpAnimDoor");
        pianoScare.Play();
        doorSlam.Play();
        gunPickup.SetActive(true);
        this.GetComponent<BoxCollider>().enabled = false;
    }
}
