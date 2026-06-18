using UnityEngine;


// This script is responsible for casting a ray from the player's position in the forward direction to determine if they are looking at an interactable object. It calculates the distance from the player to the object they are looking at and updates the distanceFromTarget variable accordingly. This information can be used by other scripts to determine if they should display interaction prompts or allow the player to interact with the object

public class PlayerCasting : MonoBehaviour
{
    public static float distanceFromTarget;
    [SerializeField] float toTarget;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            toTarget = hit.distance;
            distanceFromTarget = toTarget;
        }
    }
}
