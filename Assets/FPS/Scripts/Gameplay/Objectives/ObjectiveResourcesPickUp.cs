using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveResourcesPickUp : Objective
    {
        [Tooltip("Total number of resources to pickup to complete the objective")]
        public int numberToPickUp;

        [Tooltip("Max number of pickup actions allowed")]
        public int maxPickup;

        [Tooltip("the player GameObject")]
        public GameObject player;

        [Tooltip("The transform for the resources allocation positions")]
        public Transform[] resourcePositions;

        [Tooltip("The UI element for the pickup command")]
        public GameObject pickupCommand;

        [Tooltip("The text element for bolt counter")]
        public Text boltCounter;

        [Tooltip("The text element for gear counter")]
        public Text gearCounter;

        [Tooltip("The text element for power counter")]
        public Text powerCounter;

        [Tooltip("The text element for pickup counter")]
        public Text pickupCounter;

        public GameObject bolt;
        public GameObject gear;
        public GameObject power;

        // current number of pickup actions done
        private int currentPickup = 0;
        // List of items to pickup
        private List<ResourcePickup.Type> toPickup =  new List<ResourcePickup.Type>();
        // list of items picked up
        private List<ResourcePickup.Type> pickedUp = new List<ResourcePickup.Type>();


        private int countGear;
        private int countBolt;
        private int countPower;

        private int pickedBolt = 0;
        private int pickedGear = 0;
        private int pickedPower = 0;



        protected override void Start()
        {
            base.Start();

            EventManager.AddListener<PickupResourceEvent>(OnPickupEvent);

            createPickupList();
            displayResources();

            pickupCounter.text = "Pickups:" + maxPickup.ToString();
        }


        void displayResources()
        {
            int i = 0;
            //make sure the elements to complete the objective are all there always
            foreach(ResourcePickup.Type resource in toPickup)
            {
                if ((int)resource == (int)ResourcePickup.Type.Bolt)
                {
                    GameObject newResource = Instantiate(bolt, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Bolt;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Gear)
                {
                    GameObject newResource = Instantiate(gear, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Gear;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Power)
                {
                    GameObject newResource = Instantiate(power, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Power;
                }
                i++;
            }

                // display the remaining resources in the scene randomly
            for (i = numberToPickUp; i < resourcePositions.Length; i++)
            {
                ResourcePickup.Type resource = (ResourcePickup.Type)Random.Range(0, 3);

                if ((int)resource == (int)ResourcePickup.Type.Bolt)
                {
                    GameObject newResource = Instantiate(bolt, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Bolt;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Gear)
                {
                    GameObject newResource = Instantiate(gear, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Gear;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Power)
                {
                    GameObject newResource = Instantiate(power, resourcePositions[i].position, Quaternion.identity);
                    newResource.transform.parent = resourcePositions[i];
                    newResource.transform.parent.GetComponent<ResourcePickup>().type = ResourcePickup.Type.Power;
                }
            }

        }

        void createPickupList()
        {
            // fill the list of items to pick up randomly
            for (int i = 0; i < numberToPickUp; i++)
            {
                ResourcePickup.Type resource = (ResourcePickup.Type)Random.Range(0, 3);
                toPickup.Add(resource);
                Debug.Log("To Pickup: " + resource);

                if ((int)resource == (int)ResourcePickup.Type.Bolt)
                {
                    countBolt++;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Gear)
                {
                    countGear++;
                }
                else if ((int)resource == (int)ResourcePickup.Type.Power)
                {
                    countPower++;
                }
            }

            boltCounter.text = "0/" + countBolt;
            gearCounter.text = "0/" + countGear;
            powerCounter.text = "0/" + countPower;
        }

        void OnPickupEvent(PickupResourceEvent evt)
        { 
                if (IsCompleted)
                    return;

                // handle pickup
                ResourcePickup picked = (ResourcePickup) evt.Pickup.GetComponent<ResourcePickup>();
                Debug.Log("Picked up a " + picked.type);

                pickedUp.Add(picked.type);
                updatePickUpCounting(picked.type);

                // update counters
                boltCounter.text = pickedBolt + "/" + countBolt;
                gearCounter.text = pickedGear + "/" + countGear;
                powerCounter.text = pickedPower + "/" + countPower;

                pickupCommand.SetActive(false);
                Destroy(evt.Pickup);
                currentPickup++;
                pickupCounter.text = "Pickups:" + (maxPickup-currentPickup).ToString();

            // Check if there are still available pickup actions
            if (currentPickup == maxPickup)
                {
                    if (allPickedUp())
                    {
                        Debug.Log("completed");
                        CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

                    }
                    else
                    { // objective failed, kill the player
                        Health health = player.GetComponent<Health>();
                        health.CurrentHealth = 0;
                        health.HandleDeath(); 
                    }
                    
                }
 

        }

        /*
        bool allPickedUp()
        {
            // TO BE FIXED
            return toPickup.Intersect(pickedUp).Any();
        }
        */

        void updatePickUpCounting(ResourcePickup.Type picked)
        {
            if ((int)picked == (int)ResourcePickup.Type.Bolt)
            {
                pickedBolt++;
            }
            else if ((int)picked == (int)ResourcePickup.Type.Gear)
            {
                pickedGear++;
            }
            else if ((int)picked == (int)ResourcePickup.Type.Power)
            {
                pickedPower++;
            }
        }

        bool allPickedUp()
        {

            Debug.Log(pickedBolt + " = " + countBolt + "  " + pickedGear + " = " + countGear + "  " + pickedPower + " = " + countPower);

            return pickedBolt == countBolt && pickedGear == countGear && pickedPower == countPower;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<PickupResourceEvent>(OnPickupEvent);
        }
    }
}