/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Added to Player to Hold keys
 * */
public class DoorKeyHolder : MonoBehaviour 
{
    
    public event EventHandler OnDoorKeyAdded;
    public event EventHandler OnDoorKeyUsed;
    public GameObject door;
    public GameObject keyImage;
    public GameObject objectiveImage;
    public Sprite doorSprite;

    [Header("Key Holder")]
    [Tooltip("List of Keys currently being held")]
    public List<Key> doorKeyHoldingList = new List<Key>();

    private void Start()
    {
        keyImage.SetActive(false);
    }

    void OnTriggerEnter(Collider collider) 
    {
        DoorKey doorKey = collider.GetComponent<DoorKey>();
        if (doorKey != null) 
        {
            doorKeyHoldingList.Add(doorKey.key);
            doorKey.DestroySelf();
            keyImage.SetActive(true);
            activateNewObjective();
            OnDoorKeyAdded?.Invoke(this, EventArgs.Empty);
        }

        DoorLock doorLock = collider.GetComponent<DoorLock>();
        if (doorLock != null) 
        {
            if (doorKeyHoldingList.Contains(doorLock.key)) 
            {
                // Has key! Open door!
                doorLock.OpenDoor();
                if (doorLock.removeKeyOnUse) 
                {
                    doorKeyHoldingList.Remove(doorLock.key);
                }
                OnDoorKeyUsed?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    void activateNewObjective()
    {
        //activate new objective
        door.SetActive(true);

        objectiveImage.GetComponent<Image>().sprite = doorSprite;
    }

}
