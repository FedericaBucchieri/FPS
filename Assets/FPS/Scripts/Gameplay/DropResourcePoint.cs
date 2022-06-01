using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class DropResourcePoint : MonoBehaviour
    {
        [Tooltip("ObjectiveResourcesPickUp game object")]
        public ObjectiveResourcesPickUp objective;
        [Tooltip("UI element for the drop command")]
        public GameObject dropResourcesCommand;

        PlayerCharacterController pickingPlayer;

        private void Start()
        {
            dropResourcesCommand.SetActive(false);
            EventManager.AddListener<DropResourceEvent>(OnDropEvent);
        }

        private void OnTriggerStay(Collider other)
        {
            pickingPlayer = other.GetComponent<PlayerCharacterController>();

            if (pickingPlayer != null)
            {
                dropResourcesCommand.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            dropResourcesCommand.SetActive(false);
            pickingPlayer = null;
        }

        void OnDropEvent(DropResourceEvent evt)
        {
            if (objective.allPickedUp())
            {
                objective.CompleteObjective(string.Empty, string.Empty, "Objective complete : " + objective.Title);
            }
            else
            {
                Health health = pickingPlayer.GetComponent<Health>();
                health.CurrentHealth = 0;
                health.HandleDeath();
            }
        }
    }
}
