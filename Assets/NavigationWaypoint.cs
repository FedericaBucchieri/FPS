using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Gameplay;
using UnityEngine;

public class NavigationWaypoint : MonoBehaviour
{
    [SerializeField] private LineController line;

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacterController player = other.GetComponent<PlayerCharacterController>();

        // if the player entered the trigger
        if (player != null)
        {
            Debug.Log("Player entered");
            line.CheckWaypoint(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerCharacterController player = other.GetComponent<PlayerCharacterController>();

        // if the player entered the trigger
        if (player != null)
        {
            Debug.Log("Player exited");
            line.CheckWaypoint(this.transform);
        }
    }

}
